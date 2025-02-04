using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecusrosUD_API.Context;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class ReservaService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<ICollection<ReservaDto>> GetReservasAsync()
        {
            return await _context.Reservas.Select(r => new ReservaDto
            {
                Id = r.Id,
                IdentificadorRecurso = r.Recurso.Identificador,
                NombreRecurso = r.Recurso.Nombre,
                NombreUsuario = r.Usuario.Nombre,
                Fecha = r.Fecha,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin
            }).ToListAsync();
        }

        public async Task<ReservaDto?> GetReservaByIdAsync(long id)
        {
            return await _context.Reservas.Where(r => r.Id == id).Select(r => new ReservaDto
            {
                Id = r.Id,
                IdentificadorRecurso = r.Recurso.Identificador,
                NombreRecurso = r.Recurso.Nombre,
                NombreUsuario = r.Usuario.Nombre,
                Fecha = r.Fecha,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin
            }).FirstOrDefaultAsync();

        }

        public async Task<ReservaDto> CreateReservaAsync(Reserva nuevaReserva)
        {
            await _context.Reservas.AddAsync(nuevaReserva);

            await _context.SaveChangesAsync();

            var reservaConRelaciones = await _context.Reservas.Include(r => r.Recurso)
            .Include(r => r.Usuario).FirstOrDefaultAsync(r => r.Id == nuevaReserva.Id);

            return ReservaToDto(reservaConRelaciones); ;
        }

        public async Task<bool> ValidateReserva(Reserva reserva)
        {
            var diasSemana = new Dictionary<string, string>
        {
            { "Monday", "lunes" },
            { "Tuesday", "martes" },
            { "Wednesday", "miercoles" },
            { "Thursday", "jueves" },
            { "Friday", "viernes" },
            { "Saturday", "sabado" },
            { "Sunday", "domingo" }
        };

            var reservas = await _context.Reservas.Where(r => r.RecursoId == reserva.RecursoId).ToListAsync();

            var datosTiempo = await _context.Recursos.Where(r => r.Id == reserva.RecursoId).Select(r => new { r.TipoRecurso.HorarioDisponibilidad, r.TipoRecurso.Unidad.TiempoMin }).FirstOrDefaultAsync();

            if (reserva.HoraFin < reserva.HoraInicio) return false;


            if ((reserva.HoraFin.TotalMinutes - reserva.HoraInicio.TotalMinutes) % datosTiempo.TiempoMin.TotalMinutes != 0) return false;

            var horario = JsonConvert.DeserializeObject<Dictionary<string, Dia>>(datosTiempo.HorarioDisponibilidad);

            var diaHorario = horario[diasSemana[reserva.Fecha.DayOfWeek.ToString()]];

            if(diaHorario.Inicio > reserva.HoraInicio || diaHorario.Fin < reserva.HoraFin) return false;

            if (reservas != null || reservas.Count() != 0)
            {
                foreach (Reserva r in reservas)
                {
                    if (reserva.Fecha != r.Fecha) continue;

                    if (!(reserva.HoraInicio >= r.HoraFin || reserva.HoraFin <= r.HoraInicio)) return false;
                }
            }
            

            return true;
        }

        public static ReservaDto ReservaToDto(Reserva? reserva)
        {
            if (reserva == null)
            {
                throw new ArgumentNullException(nameof(reserva));
            }

            return new ReservaDto
            {
                Id = reserva.Id,
                IdentificadorRecurso = reserva.Recurso.Identificador,
                NombreRecurso = reserva.Recurso.Nombre,
                NombreUsuario = reserva.Usuario.Nombre,
                Fecha = reserva.Fecha,
                HoraInicio = reserva.HoraInicio,
                HoraFin = reserva.HoraFin
            };
        }
    }
}
