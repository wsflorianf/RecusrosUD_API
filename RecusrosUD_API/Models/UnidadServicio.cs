using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Models;

public partial class UnidadServicio
{
    public long Id { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string HorarioDisponibilidad { get; set; } = null!;

    [Required]
    public TimeOnly TiempoMin { get; set; }

    public virtual ICollection<TipoRecurso> TiposRecursos { get; set; } = new List<TipoRecurso>();
}
