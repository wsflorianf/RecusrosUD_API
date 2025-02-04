using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class TipoRecursoService(AppDbContext context)
    {

        private readonly AppDbContext _context = context;

        public async Task<TipoRecursoDto?> GetTipoByIdAsync(long id)
        {
            return await _context.TiposRecursos.Where(tr => tr.Id == id).Select(tr => new TipoRecursoDto
            {
                Id = tr.Id,
                Nombre = tr.Nombre,
                Descripcion = tr.Descripcion,
                HorarioDisponibilidad = tr.HorarioDisponibilidad,
                Caracteristicas = tr.Caracteristicas,
                NombreUnidad = tr.Unidad.Nombre
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TipoRecursoDto>> GetTiposRecursoAsync()
        {
            return await _context.TiposRecursos.Select(tr => new TipoRecursoDto
            {
                Id = tr.Id,
                Nombre = tr.Nombre,
                Descripcion = tr.Descripcion,
                HorarioDisponibilidad = tr.HorarioDisponibilidad,
                Caracteristicas = tr.Caracteristicas,
                NombreUnidad = tr.Unidad.Nombre
            }).ToListAsync();
        }

        public async Task<TipoRecursoDto> CreateTipoRecursoAsync(TipoRecurso nuevoTipo)
        {
            _context.TiposRecursos.Add(nuevoTipo);

            await _context.SaveChangesAsync();

            var creadoConRelaciones = await _context.TiposRecursos.Include(tr => tr.Unidad).FirstOrDefaultAsync(tr => tr.Id == nuevoTipo.Id);

            return TipoRecursoToDto(creadoConRelaciones);
        }

        public static TipoRecursoDto TipoRecursoToDto(TipoRecurso? tipo)
        {
            if(tipo == null)
            {
                throw new ArgumentNullException(nameof(tipo));
            }
            return new TipoRecursoDto
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Descripcion = tipo.Descripcion,
                HorarioDisponibilidad = tipo.HorarioDisponibilidad,
                Caracteristicas = tipo.Caracteristicas,
                NombreUnidad = tipo.Unidad.Nombre
            };
        }
    }
}
