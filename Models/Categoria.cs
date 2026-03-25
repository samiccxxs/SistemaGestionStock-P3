using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SistemaStock.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}