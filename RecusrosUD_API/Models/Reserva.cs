using System;
using System.Collections.Generic;

namespace RecusrosUD_API.Models;

public partial class Reserva
{
    public long Id { get; set; }

    public long UsuarioId { get; set; }

    public long RecursoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual Recurso? Recurso { get; set; } 

    public virtual Usuario? Usuario { get; set; } 
}
