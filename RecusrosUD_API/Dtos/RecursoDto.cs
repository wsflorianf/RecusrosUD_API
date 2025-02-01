namespace RecusrosUD_API.Dtos
{
    public class RecursoDto
    {
        public long Id { get; set; }

        public string NombreTipo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Identificador { get; set; } = null!;

        public string? Caracteristicas { get; set; }
    }
}
