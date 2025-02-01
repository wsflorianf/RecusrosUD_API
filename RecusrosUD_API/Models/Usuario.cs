
using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Models;

public partial class Usuario
{
    public long Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El campo Correo es obligatorio.")]
    [EmailAddress]
    public string Correo { get; set; } = null!;

    [Required(ErrorMessage = "El campo Contra es obligatorio.")]
    public string Contra { get; set; } = null!;

    [Required(ErrorMessage = "El campo Admin es obligatorio.")]
    public bool Admin { get; set; }

    public virtual ICollection<Prestamo> PrestamoEmpleadoEntregas { get; set; } = new List<Prestamo>();

    public virtual ICollection<Prestamo> PrestamoEmpleadoRecepcions { get; set; } = new List<Prestamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
