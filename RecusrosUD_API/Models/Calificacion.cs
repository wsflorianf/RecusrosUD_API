using System;
using System.Collections.Generic;

namespace RecusrosUD_API.Models;

public partial class Calificacion
{
    public long Id { get; set; }

    public long ReservaId { get; set; }

    public int? CumplimientoHorarios { get; set; }

    public int? CalidadEstadoRecurso { get; set; }

    public int? AmabilidadPersonal { get; set; }

    public virtual Reserva Reserva { get; set; } = null!;
}
