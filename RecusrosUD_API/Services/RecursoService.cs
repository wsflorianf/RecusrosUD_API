using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class RecursoService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<ICollection<RecursoDto>> GetRecursosAsync()
        {
            return await _context.Recursos.Select(r => new RecursoDto
            {
                Id = r.Id,
                Identificador = r.Identificador,
                NombreTipo = r.TipoRecurso.Nombre,
                Nombre = r.Nombre,
                Caracteristicas = r.Caracteristicas
            }).ToListAsync();
        }

        public async Task<RecursoDto?> GetRecursoByIdAsync(long id)
        {
            return await _context.Recursos.Select(r => new RecursoDto
            {
                Id = r.Id,
                Identificador = r.Identificador,
                NombreTipo = r.TipoRecurso.Nombre,
                Nombre = r.Nombre,
                Caracteristicas = r.Caracteristicas
            }).FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<RecursoDto> CreateRecursoAsync(Recurso newRecurso)
        {
            await _context.Recursos.AddAsync(newRecurso);

            await _context.SaveChangesAsync();

            var creado = RecursoToDto(newRecurso);

            return creado;
        }

        public static RecursoDto RecursoToDto(Recurso recurso)
        {
            return new RecursoDto
            {
                Id = recurso.Id,
                Identificador = recurso.Identificador,
                NombreTipo = recurso.TipoRecurso.Nombre,
                Nombre = recurso.Nombre,
                Caracteristicas = recurso.Caracteristicas
            };
        }
    }
}
