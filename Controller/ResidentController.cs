using MSResidents.Models;
using MSResidents.Services;
using Microsoft.AspNetCore.Mvc;
using MSResidents.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MSResidents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly ResidentService _residentService;

        public ResidentsController(ResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("{residentId}")]
        public async Task<ActionResult<ResidentDto>> GetResidentById(int residentId)
        {
            var resident = await _residentService.GetResidentByIdAsync(residentId);

            if (resident == null)
            {
                return NotFound(); 
            }

            return Ok(resident); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidentDto>>> SearchResidents(
            [FromQuery] string? name,
            [FromQuery] int? apartmentNumber,
            [FromQuery] int? blockNumber,
            [FromQuery] DateTime? moveInDate)
        {
            var residents = await _residentService.SearchResidentsAsync(name, apartmentNumber, blockNumber, moveInDate);
            return Ok(residents); 
        }

        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<ResidentDto>>> GetResidentsPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var residents = await _residentService.GetResidentsPagedAsync(page, pageSize);
            return Ok(residents); 
        }

        [HttpPost]
        public async Task<ActionResult<ResidentDto>> CreateResident([FromBody] ResidentDto residentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdResident = await _residentService.CreateResidentAsync(residentDto);

            if (createdResident.Value is null)
            {
                return BadRequest("Erro ao criar residente.");
            }

            return CreatedAtAction(nameof(GetResidentById), new { residentId = createdResident.Value.ResidentId }, createdResident.Value);
        }

        [HttpPut("{residentId}")]
        public async Task<ActionResult<ResidentDto>> UpdateResident(int residentId, [FromBody] ResidentDto residentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedResident = await _residentService.UpdateResidentAsync(residentId, residentDto);
            if (updatedResident == null)
            {
                return NotFound(); 
            }

            return Ok(updatedResident);
        }

        [HttpDelete("{residentId}")]
        public async Task<ActionResult> DeleteResident(int residentId)
        {
            var actionResult = await _residentService.DeleteResidentAsync(residentId);

            if (actionResult is NotFoundResult)
            {
                return NotFound(new { message = "Residente não encontrado." });
            }

            return NoContent();
        }
    }
}
