using Application.DTOs.Pilot;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Pilot")]
    public class PilotController : ControllerBase
    {
        private readonly PilotService _pilotService;

        public PilotController(PilotService pilotService)
        {
            _pilotService = pilotService;
        }

        [HttpGet("my-shipments")]
        public async Task<IActionResult> GetMyShipments()
        {   
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shipments = await _pilotService.GetMyShipments(userId);
            return Ok(shipments);
        }

        [HttpPatch("status/{shipmentId}")]
        public async Task<IActionResult> UpdateStatus(int shipmentId, [FromBody] UpdateStatusDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _pilotService.UpdateShipmentStatus(shipmentId, userId, dto.StatusId);

            if (!result) return NotFound(new { message = "Envío no encontrado o no asignado a este piloto." });

            return Ok(new { message = "Estado actualizado correctamente." });
        }

        [HttpPost("confirm/{shipmentId}")]
        public async Task<IActionResult> ConfirmDelivery(int shipmentId, [FromBody] ConfirmDeliveryDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _pilotService.ConfirmDelivery(shipmentId, userId, dto);

            if (!result) return NotFound(new { message = "Envío no encontrado o no asignado a este piloto." });

            return Ok(new { message = "Entrega confirmada correctamente." });
        }

        [HttpPost("return/{shipmentId}")]
        public async Task<IActionResult> ReportReturn(int shipmentId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _pilotService.ReportReturn(shipmentId, userId);

            if (!result) return NotFound(new { message = "Envío no encontrado o no asignado a este piloto." });

            return Ok(new { message = "Devolución reportada correctamente." });
        }
    }
}