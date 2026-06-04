using Application.DTOs.User;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Llamamos al método Login del servicio que acabas de reparar
            var token = await _userService.Login(dto);

            // Si las credenciales fallan, devolvemos un 401 sin dar pistas de qué falló por seguridad
            if (token == null)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });
            }

            // Si todo sale bien, le regresamos el JWT firmado al frontend
            return Ok(new
            {
                message = "Autenticación exitosa",
                token = token
            });
        }
    }
}
