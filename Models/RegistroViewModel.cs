using System.ComponentModel.DataAnnotations;

namespace SistemaStock.Models
{
    public class RegistroViewModel
    {
        [Required]
        public string? NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmarPassword { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public int RolId { get; set; } = 2;
    }
}
