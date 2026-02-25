using System.ComponentModel.DataAnnotations;

namespace SistemaStock.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
