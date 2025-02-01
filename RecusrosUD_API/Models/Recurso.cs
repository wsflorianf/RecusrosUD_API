using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Models;

public partial class Recurso
{
    public long Id { get; set; }

    [Required]
    public long TipoRecursoId { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string Identificador { get; set; } = null!;

    public string? Caracteristicas { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual TipoRecurso TipoRecurso { get; set; } = null!;
}
