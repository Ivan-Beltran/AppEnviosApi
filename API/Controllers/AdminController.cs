using Application.DTOs.Admin;
using Application.DTOs.User;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;


        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllAdminGlobal")]
        [Authorize(Roles = "GlobalAdmin")]

        public async Task<List<AdminGlobalDTO>> GetAllAdminGlobal()
        {
            return await _userService.GetAllAdminGlobal();
        }

        [HttpGet("GetByIdAdminGlobal")]
        [Authorize(Roles = "GlobalAdmin")]

        public async Task<AdminGlobalDTO?> GetByIdAdminGlobal(int id)
        {
            return await _userService.GetByIdAdminGlobal(id);
        }


        [HttpPost("register-adminGlobal")]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> CreateAdminGlobal([FromBody] CreateUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.CreateAdminGlobal(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPut("update-adminGlobal/{id}")]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> UpdateAdminGlobal(int id, [FromBody] UpdateUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.UpdateAdminGlobal(id, dto);
                if (result == -1)
                {
                    return NotFound(new { message = "Admin no encontrado o no es un admin global" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpDelete("global-admins/{id}")]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> DeleteAdminGlobal(int id)
        {
            // Aquí SÍ llamas al _userService
            var result = await _userService.DeleteAdminGlobal(id);

            if (!result)
            {
                return NotFound(new { message = $"No se pudo eliminar. El Admin con ID {id} no existe." });
            }

            return Ok(new { message = "Administrador global desactivado con éxito." });
        }
    }
}
