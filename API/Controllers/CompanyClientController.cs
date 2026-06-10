using Application.DTOs.CompanyClient;
using Application.DTOs.Pilot;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
    public class CompanyClientController : ControllerBase
    {
        private readonly CompanyClientService _companyClientService;

        public CompanyClientController(CompanyClientService companyClientService)
        {
            _companyClientService = companyClientService;
        }

        [HttpGet("GetAllClient")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<List<CompanyClientDTO>> GetAllAdminArea()
        {
            return await _companyClientService.GetAllCompanyClient();
        }

        [HttpGet("GetByIdClient")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<CompanyClientDTO?> GetByIdAdminArea(int id)
        {
            return await _companyClientService.GetByIdCompanyClient(id);
        }

        [HttpPost("create-client")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<IActionResult> CreateClient([FromBody] CreateCompanyClientDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _companyClientService.CreateCompanyClient(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpPut("update-client/{id}")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<IActionResult> UpdateCompanyClient(int id, [FromBody] UpdateCompanyClientDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _companyClientService.UpdateCompanyClient(id, dto);

                if (result == -1)
                {
                    return NotFound(new
                    {
                        message = "Admin de área no encontrado"
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("Client/{id}")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<IActionResult> DeleteCompanyClient(int id)
        {
            try
            {
                var result = await _companyClientService.DeleteCompanyClient(id);

                if (!result)
                {
                    return NotFound(new
                    {
                        message = $"No se pudo eliminar. El Admin de área con ID {id} no existe."
                    });
                }

                return Ok(new
                {
                    message = "cliente desactivado con éxito."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }
    }
}
