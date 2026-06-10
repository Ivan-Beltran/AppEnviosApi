using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.DTOs.Receiver;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]
    public class ReceiverController : ControllerBase
    {
        private readonly ReceiverService _receiverService;

        public ReceiverController(ReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        [HttpGet("GetAllReceiver")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]

        public async Task<List<ReceiverDTO>> GetAllReceiver()
        {
            return await _receiverService.GetAllReceivers();
        }

        [HttpGet("GetByIdReceiver")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]

        public async Task<ReceiverDTO?> GetByIdReceiver(int id)
        {
            return await _receiverService.GetReceiverById(id);
        }

        [HttpPost("create-receiver")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]
        public async Task<IActionResult> CreateReceiver([FromBody] CreateReciverDTO dto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("No se encontró el Id del usuario en el token.");
                }

                var userId = int.Parse(userIdClaim);

                var result = await _receiverService.CreateReceiver(dto, userId);

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

        [HttpPut("update-receiver/{id}")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]

        public async Task<IActionResult> UpdateReceiver(int id, [FromBody] UpdateReciverDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _receiverService.UpdateReceiver(id, dto);

                if (result == -1)
                {
                    return NotFound(new
                    {
                        message = "destinatario de no encontrado"
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
    }
}
