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
                Caracteristicas = r.Caracteristicas,
                HorarioDisponibilidad = r.TipoRecurso.HorarioDisponibilidad,
                TiempoMin = r.TipoRecurso.Unidad.TiempoMin
            }).ToListAsync();
        }

        public async Task<RecursoDto?> GetRecursoByIdAsync(long id)
        {
            return await _context.Recursos.Where(r => r.Id == id).Select(r => new RecursoDto
            {
                Id = r.Id,
                Identificador = r.Identificador,
                NombreTipo = r.TipoRecurso.Nombre,
                Nombre = r.Nombre,
                Caracteristicas = r.Caracteristicas,
                HorarioDisponibilidad = r.TipoRecurso.HorarioDisponibilidad,
                TiempoMin = r.TipoRecurso.Unidad.TiempoMin
            }).FirstOrDefaultAsync();

        }

        public async Task<RecursoDto> CreateRecursoAsync(Recurso nuevoRecurso)
        {
            await _context.Recursos.AddAsync(nuevoRecurso);

            await _context.SaveChangesAsync();

            var recursoConRelaciones = await _context.Recursos.Include(r => r.TipoRecurso)
            .ThenInclude(tr => tr.Unidad).FirstOrDefaultAsync(r => r.Id == nuevoRecurso.Id);


            return RecursoToDto(recursoConRelaciones); ;
        }

        public static RecursoDto RecursoToDto(Recurso? recurso)
        {
            if(recurso == null)
            {
                throw new ArgumentNullException(nameof(recurso));
            }

            return new RecursoDto
            {
                Id = recurso.Id,
                Identificador = recurso.Identificador,
                NombreTipo = recurso.TipoRecurso.Nombre,
                Nombre = recurso.Nombre,
                Caracteristicas = recurso.Caracteristicas,
                HorarioDisponibilidad = recurso.TipoRecurso.HorarioDisponibilidad,
                TiempoMin = recurso.TipoRecurso.Unidad.TiempoMin
            };
        }
    }
}
