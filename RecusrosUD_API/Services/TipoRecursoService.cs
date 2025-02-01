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
            return await _context.TiposRecursos.Select(tr => new TipoRecursoDto
            {
                Id = tr.Id,
                Nombre = tr.Nombre,
                Descripcion = tr.Descripcion,
                HorarioDisponibilidad = tr.HorarioDisponibilidad,
                Caracteristicas = tr.Caracteristicas,
                NombreUnidad = tr.Unidad.Nombre
            }).FirstOrDefaultAsync(tr => tr.Id == id);
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

            var creado = RecursoToDto(nuevoTipo);

            return creado;
        }

        public static TipoRecursoDto RecursoToDto(TipoRecurso tipo)
        {
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
