using System.ComponentModel.DataAnnotations;

namespace RecusrosUD_API.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "El campo Contra es obligatorio.")]
        public string Contra { get; set; } = null!;
    }
}
