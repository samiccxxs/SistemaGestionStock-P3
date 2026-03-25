using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SistemaStock.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El código es obligatorio.")]
    [StringLength(50, ErrorMessage = "El código no puede tener más de 50 caracteres.")]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [StringLength(200, ErrorMessage = "La descripción no puede tener más de 200 caracteres.")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
    [Display(Name = "Stock Mínimo")]
    public int StockMinimo { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
}