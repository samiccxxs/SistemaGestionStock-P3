using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStock.Filters;
using SistemaStock.Models;

namespace SistemaStock.Controllers
{
    [SessionAuthFilter]
    public class MovimientosController : Controller
    {
        private readonly SistemaStockContext _context;

        public MovimientosController(SistemaStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movimientos = await _context.Movimientos
                .Include(m => m.Producto)
                .Include(m => m.Usuario)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();

            ViewBag.Productos = await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync();

            return View(movimientos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrada([Bind("ProductoId,Cantidad,Observacion")] Movimiento movimiento)
        {
            ModelState.Remove("Tipo");
            ModelState.Remove("Producto");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                var producto = await _context.Productos.FindAsync(movimiento.ProductoId);

                if (producto == null)
                {
                    TempData["Error"] = "Producto no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                movimiento.Tipo = "Entrada";
                movimiento.StockAnterior = producto.Stock;
                movimiento.StockNuevo = producto.Stock + movimiento.Cantidad;
                movimiento.Fecha = DateTime.Now;
                movimiento.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

                producto.Stock = movimiento.StockNuevo;

                _context.Add(movimiento);
                _context.Update(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Entrada registrada. Stock actualizado a {movimiento.StockNuevo} unidades.";
            }
            else
            {
                TempData["Error"] = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salida([Bind("ProductoId,Cantidad,Observacion")] Movimiento movimiento)
        {
            ModelState.Remove("Tipo");
            ModelState.Remove("Producto");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                var producto = await _context.Productos.FindAsync(movimiento.ProductoId);

                if (producto == null)
                {
                    TempData["Error"] = "Producto no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                if (movimiento.Cantidad > producto.Stock)
                {
                    TempData["Error"] = $"Stock insuficiente. Stock actual: {producto.Stock} unidades.";
                    return RedirectToAction(nameof(Index));
                }

                movimiento.Tipo = "Salida";
                movimiento.StockAnterior = producto.Stock;
                movimiento.StockNuevo = producto.Stock - movimiento.Cantidad;
                movimiento.Fecha = DateTime.Now;
                movimiento.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

                producto.Stock = movimiento.StockNuevo;

                _context.Add(movimiento);
                _context.Update(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Salida registrada. Stock actualizado a {movimiento.StockNuevo} unidades.";
            }
            else
            {
                TempData["Error"] = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}