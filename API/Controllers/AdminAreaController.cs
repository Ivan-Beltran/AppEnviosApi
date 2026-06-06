using Application.DTOs.AdminArea;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AdminAreaController : ControllerBase
	{
		private readonly AdminAreaService _adminAreaService;

		public AdminAreaController(AdminAreaService adminAreaService)
		{
			_adminAreaService = adminAreaService;
		}

		[HttpGet("GetAllAdminArea")]
		[Authorize(Roles = "GlobalAdmin")]
		public async Task<List<AdminAreaDTO>> GetAllAdminArea()
		{
			return await _adminAreaService.GetAllAdminArea();
		}

		[HttpGet("GetByIdAdminArea")]
		[Authorize(Roles = "GlobalAdmin")]
		public async Task<AdminAreaDTO?> GetByIdAdminArea(int id)
		{
			return await _adminAreaService.GetByIdAdminArea(id);
		}

		[HttpPost("create-adminArea")]
		[Authorize(Roles = "GlobalAdmin")]
		public async Task<IActionResult> CreateAdminArea([FromBody] CreateAdminAreaDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var result = await _adminAreaService.CreateAdminArea(dto);

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

		[HttpPut("update-adminArea/{id}")]
		[Authorize(Roles = "GlobalAdmin")]
		public async Task<IActionResult> UpdateAdminArea(int id, [FromBody] UpdateAdminAreaDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var result = await _adminAreaService.UpdateAdminArea(id, dto);

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

		[HttpDelete("adminArea/{id}")]
		[Authorize(Roles = "GlobalAdmin")]
		public async Task<IActionResult> DeleteAdminArea(int id)
		{
			try
			{
				var result = await _adminAreaService.DeleteAdminArea(id);

				if (!result)
				{
					return NotFound(new
					{
						message = $"No se pudo eliminar. El Admin de área con ID {id} no existe."
					});
				}

				return Ok(new
				{
					message = "Administrador de área desactivado con éxito."
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