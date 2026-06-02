using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.User;
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

        [HttpPost("register-adminGlobal")]
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
    }
}
