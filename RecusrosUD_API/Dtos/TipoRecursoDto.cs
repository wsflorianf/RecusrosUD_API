namespace RecusrosUD_API.Dtos
{
    public class TipoRecursoDto
    {
        public long Id { get; set; }

        public string NombreUnidad { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string HorarioDisponibilidad { get; set; } = null!;

        public string? Caracteristicas { get; set; }
    }

    public class Dia
    {
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fin { get; set; }
    }
}
