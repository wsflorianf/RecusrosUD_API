using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Dtos
{
    public class UnidadServicioDto
    {
        public long Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string HorarioDisponibilidad { get; set; } = null!;

        [Required]
        public TimeOnly TiempoMin { get; set; }
    }
}
