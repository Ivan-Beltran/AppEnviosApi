using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Shipment;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient,Pilot")]
    public class ShipmentController : Controller
    {
        private readonly ShipmentService _shipmentService;
        private readonly IWebHostEnvironment _environment;

        public ShipmentController(ShipmentService shipmentService, IWebHostEnvironment environment)
        {
            _shipmentService = shipmentService;
            _environment = environment;
        }

        [HttpGet("GetAllShipment")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient,Pilot")]
        public async Task<IActionResult> GetAllShipment()
        {

            var roleId = int.Parse(
                User.FindFirst("RoleId")!.Value
            );

            var branchId = int.Parse(
                User.FindFirst("BranchId")!.Value
            );

            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            Console.WriteLine($"RoleId: {roleId}");
            Console.WriteLine($"BranchId: {branchId}");
            Console.WriteLine($"UserId: {userId}");

            var result = await _shipmentService.GetAllAsync(
                roleId,
                branchId,
                userId
            );

            return Ok(result);
        }


        [HttpGet("GetByIdShipment")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient,Pilot")]
        public async Task<ShipmentDTO?> GetByIdShipment(int id)
        {
            return await _shipmentService.GetByIdAsync(id);
        }

        [HttpPost("CreateShipment")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]
        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("No se encontró el Id del usuario en el token.");
                }

                var userId = int.Parse(userIdClaim);

                var shipmentId = await _shipmentService.CreateAsync(dto, userId);

                return Ok(new
                {
                    shipmentId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [HttpPut("UpdateShipment")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,CompanyClient")]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] UpdateShipmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _shipmentService.UpdateAsync(id, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Shipment no encontrado" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message,
                });
            }
        }

        [HttpPut("{id}/pilot")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin")]
        public async Task<IActionResult> UpdatePilot(int id,[FromBody] UpdatePilotShipmentDTO dto)
        {
            try
            {
                await _shipmentService.UpdatePilotAsync(id, dto);

                return Ok(new
                {
                    message = "Piloto asignado correctamente"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "AreaAdmin,GlobalAdmin,Pilot")]

        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            try
            {
                await _shipmentService.UpdateStatusShipment(id, dto);

                return Ok(new
                {
                    message = "stado cambiado correctamente"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("{id}/confirmation")]
        [Authorize(Roles = "AreaAdmin,AdminGlobal,Pilot")]
        public async Task<IActionResult> CreateConfirmation(int id,IFormFile? confirmationPhoto,IFormFile? receiverSignature)
        {
            try
            {
                string photoPath = string.Empty;
                string signaturePath = string.Empty;

                if (confirmationPhoto != null)
                {
                    var folder = Path.Combine(
                        _environment.ContentRootPath,  // ✅ en lugar de WebRootPath
                        "uploads", "confirmations"
                    );
                    Directory.CreateDirectory(folder);
                    var fileName = Guid.NewGuid() + Path.GetExtension(confirmationPhoto.FileName);
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await confirmationPhoto.CopyToAsync(stream);
                    photoPath = $"/uploads/confirmations/{fileName}";
                }

                if (receiverSignature != null)
                {
                    var folder = Path.Combine(
                        _environment.ContentRootPath,  
                        "uploads", "signatures"
                    );
                    Directory.CreateDirectory(folder);
                    var fileName = Guid.NewGuid() + Path.GetExtension(receiverSignature.FileName);
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await receiverSignature.CopyToAsync(stream);
                    signaturePath = $"/uploads/signatures/{fileName}";
                }

                var dto = new ConfirmationShipmentDTO
                {
                    ShipmentId = id,
                    ConfirmationPhoto = photoPath,
                    ReceiverSignature = signaturePath
                };

                var confirmationId = await _shipmentService.CreateConfirmation(dto);
                return Ok(new { confirmationId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al guardar la confirmación",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("{id}/return")]
        [Authorize(Roles = "Pilot,AreaAdmin,AdminGlobal")]
        public async Task<IActionResult> CreateReturn(
    int id,
    [FromBody] CreateReturnDTO dto)
        {
            try
            {
                dto.ShipmentId = id;

                var returnId =
                    await _shipmentService.CreateReturn(dto);

                return Ok(new
                {
                    ReturnId = returnId,
                    Message = "Devolución registrada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al registrar la devolución",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }


    }          

}
