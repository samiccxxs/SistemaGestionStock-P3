using System;
using System.ComponentModel.DataAnnotations;
namespace SistemaStock.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    [Required(ErrorMessage = "El producto es obligatorio.")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El tipo es obligatorio.")]
    public string Tipo { get; set; } = null!;

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; set; }

    public int StockAnterior { get; set; }
    public int StockNuevo { get; set; }

    [StringLength(200, ErrorMessage = "La observación no puede tener más de 200 caracteres.")]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;
    public virtual Usuarios Usuario { get; set; } = null!;
}