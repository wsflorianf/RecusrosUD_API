using System;
using System.Collections.Generic;

namespace RecusrosUD_API.Models;

public partial class Prestamo
{
    public long Id { get; set; }

    public long ReservaId { get; set; }

    public TimeOnly? HoraEntrega { get; set; }

    public TimeOnly? HoraDevolucion { get; set; }

    public long? EmpleadoEntregaId { get; set; }

    public long? EmpleadoRecepcionId { get; set; }

    public virtual Usuario? EmpleadoEntrega { get; set; }

    public virtual Usuario? EmpleadoRecepcion { get; set; }

    public virtual Reserva Reserva { get; set; } = null!;
}
