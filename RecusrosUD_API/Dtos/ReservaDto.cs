namespace RecusrosUD_API.Dtos
{
    public class ReservaDto
    {
        public long Id { get; set; }

        public string IdentificadorRecurso { get; set; } = null!;

        public string NombreRecurso { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public DateOnly Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }
    }
}
