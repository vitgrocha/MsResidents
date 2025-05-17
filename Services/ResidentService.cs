using MSResidents.Models;
using MSResidents.Dtos;
using MSResidents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; // Para usar ActionResult

namespace MSResidents.Services
{
    public class ResidentService
    {
        private readonly ApplicationDbContext _context;

        public ResidentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para buscar residentes com filtros
        public async Task<IEnumerable<ResidentDto>> SearchResidentsAsync(
            string? name,
            int? apartmentNumber,
            int? blockNumber,
            DateTime? moveInDate)
        {
            var query = _context.Residents.AsQueryable();

            // Filtro por nome
            if (!string.IsNullOrEmpty(name))
                query = query.Where(r => r.Name.Contains(name));

            // Filtro por número de apartamento
            if (apartmentNumber.HasValue)
                query = query.Where(r => r.ApartmentNumber == apartmentNumber);

                // Filtro por número de apartamento
            if (blockNumber.HasValue)
                query = query.Where(r => r.BlockNumber == blockNumber);

            // Filtro por data de entrada
            if (moveInDate.HasValue)
                query = query.Where(r => r.MoveInDate == moveInDate);

            var residents = await query.ToListAsync();
            return residents.Select(r => new ResidentDto
            {
                ResidentId = r.ResidentId,
                Name = r.Name,
                ApartmentNumber = r.ApartmentNumber,
                BlockNumber = r.BlockNumber,
                MoveInDate = r.MoveInDate
            });
        }

        // Método para listar residentes com paginação
        public async Task<IEnumerable<ResidentDto>> GetResidentsPagedAsync(int page, int pageSize)
        {
            var residents = await _context.Residents
                .Skip((page - 1) * pageSize)  // Pula os itens das páginas anteriores
                .Take(pageSize)  // Limita o número de itens por página
                .ToListAsync();

            return residents.Select(r => new ResidentDto
            {
                ResidentId = r.ResidentId,
                Name = r.Name,
                ApartmentNumber = r.ApartmentNumber,
                BlockNumber = r.BlockNumber,
                MoveInDate = r.MoveInDate
            });
        }

        // Método para criar um residente (POST)
        public async Task<ActionResult<ResidentDto>> CreateResidentAsync(ResidentDto residentDto)
        {
            var resident = new Resident
            {
                Name = residentDto.Name,
                ApartmentNumber = residentDto.ApartmentNumber,
                BlockNumber = residentDto.BlockNumber,
                MoveInDate = residentDto.MoveInDate
            };

            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetResidentById", "Residents", new { id = resident.ResidentId }, new ResidentDto
            {
                ResidentId = resident.ResidentId,
                Name = resident.Name,
                ApartmentNumber = resident.ApartmentNumber,
                BlockNumber = resident.BlockNumber,
                MoveInDate = resident.MoveInDate
            });
        }

        // Método para buscar um residente por ID (GET)
        public async Task<ActionResult<ResidentDto>> GetResidentByIdAsync(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null)
                return new NotFoundResult();  // Retorna 404 se o residente não for encontrado

            return new ResidentDto
            {
                ResidentId = resident.ResidentId,
                Name = resident.Name,
                ApartmentNumber = resident.ApartmentNumber,
                BlockNumber = resident.BlockNumber,
                MoveInDate = resident.MoveInDate
            };
        }

        // Método para atualizar um residente (PUT)
        public async Task<ActionResult<ResidentDto>> UpdateResidentAsync(int id, ResidentDto residentDto)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null)
                return new NotFoundResult();  // Retorna 404 se o residente não for encontrado

            resident.Name = residentDto.Name;
            resident.ApartmentNumber = residentDto.ApartmentNumber;
            resident.BlockNumber = residentDto.BlockNumber;
            resident.MoveInDate = residentDto.MoveInDate;

            _context.Residents.Update(resident);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new ResidentDto
            {
                ResidentId = resident.ResidentId,
                Name = resident.Name,
                ApartmentNumber = resident.ApartmentNumber,
                BlockNumber = resident.BlockNumber,
                MoveInDate = resident.MoveInDate

            });
        }

        // Método para excluir um residente (DELETE)
        public async Task<ActionResult> DeleteResidentAsync(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null)
                return new NotFoundResult();  // Retorna 404 se o residente não for encontrado

            _context.Residents.Remove(resident);
            await _context.SaveChangesAsync();

            return new NoContentResult();  // Retorna 204 após remoção bem-sucedida
        }
    }
}
