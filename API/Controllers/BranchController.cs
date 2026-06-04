using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BranchController : ControllerBase
	{
		private readonly BranchService _branchService;

		public BranchController(BranchService branchService)
		{
			_branchService = branchService;
		}

		// GET: api/Branch
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var branches = await _branchService.GetAll();

			return Ok(branches);
		}

		// GET: api/Branch/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var branch = await _branchService.GetById(id);

			if (branch == null)
			{
				return NotFound(new { mensaje = "Sucursal no encontrada" });
			}

			return Ok(branch);
		}

		// POST: api/Branch
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] BranchDTO branchDTO)
		{
			var branchCreada = await _branchService.Create(branchDTO);

			return Ok(branchCreada);
		}

		// PUT: api/Branch/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] BranchDTO branchDTO)
		{
			var branchActualizada = await _branchService.Update(id, branchDTO);

			if (branchActualizada == null)
			{
				return NotFound(new { mensaje = "Sucursal no encontrada" });
			}

			return Ok(branchActualizada);
		}

		// DELETE: api/Branch/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var eliminado = await _branchService.Delete(id);

			if (!eliminado)
			{
				return NotFound(new { mensaje = "Sucursal no encontrada" });
			}

			return Ok(new { mensaje = "Sucursal eliminada correctamente" });
		}
	}
}