using System;
using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Models;

public partial class TipoRecurso
{
    public long Id { get; set; }

    [Required]
    public long UnidadId { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    [Required]
    public string HorarioDisponibilidad { get; set; } = null!;

    public string? Caracteristicas { get; set; }

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

    public virtual UnidadServicio Unidad { get; set; } = null!;
}
