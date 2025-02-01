using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class UnidadService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<UnidadServicio>> GetUnidadesAsync()
        {
            return await _context.UnidadesServicios.ToListAsync();
        }

        public async Task<UnidadServicio?> GetUnidadByIdAsync(long id)
        {
            return await _context.UnidadesServicios.FindAsync(id);
        }

        public async Task<UnidadServicio> CreateUnidadAsync(UnidadServicio nuevaUnidad)
        {
            _context.UnidadesServicios.Add(nuevaUnidad);

            await _context.SaveChangesAsync();

            return nuevaUnidad;
        }

    }
}
