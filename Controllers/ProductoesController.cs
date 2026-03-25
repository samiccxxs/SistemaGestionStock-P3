using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStock.Filters;
using SistemaStock.Models;

namespace SistemaStock.Controllers
{
    [SessionAuthFilter]
    public class ProductoesController : Controller
    {
        private readonly SistemaStockContext _context;

        public ProductoesController(SistemaStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();
            return View(productos);
        }

        [SessionAuthFilter(1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,Descripcion,CategoriaId,Precio,Stock,StockMinimo,Activo")] Producto producto)
        {
            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                var codigoExiste = await _context.Productos
                    .AnyAsync(p => p.Codigo == producto.Codigo);

                if (codigoExiste)
                {
                    TempData["Error"] = "Ya existe un producto con ese código.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto creado correctamente.";
            }
            else
            {
                TempData["Error"] = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return RedirectToAction(nameof(Index));
        }

        [SessionAuthFilter(1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProductoId,Codigo,Nombre,Descripcion,CategoriaId,Precio,Stock,StockMinimo,Activo")] Producto producto)
        {
            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                try
                {
                    var original = await _context.Productos
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.ProductoId == producto.ProductoId);

                    if (original == null)
                    {
                        TempData["Error"] = "El producto no existe.";
                        return RedirectToAction(nameof(Index));
                    }

                    // Verificar código duplicado en otro producto
                    var codigoExiste = await _context.Productos
                        .AnyAsync(p => p.Codigo == producto.Codigo && p.ProductoId != producto.ProductoId);

                    if (codigoExiste)
                    {
                        TempData["Error"] = "Ya existe otro producto con ese código.";
                        return RedirectToAction(nameof(Index));
                    }

                    producto.FechaCreacion = original.FechaCreacion;

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Producto actualizado correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(p => p.ProductoId == producto.ProductoId))
                        TempData["Error"] = "El producto no existe.";
                    else
                        throw;
                }
            }
            else
            {
                TempData["Error"] = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return RedirectToAction(nameof(Index));
        }

        [SessionAuthFilter(1)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                TempData["Error"] = "Producto no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var tieneMovimientos = await _context.Movimientos
                .AnyAsync(m => m.ProductoId == id);

            if (tieneMovimientos)
            {
                // En vez de eliminar, desactivar
                producto.Activo = false;
                _context.Update(producto);
                await _context.SaveChangesAsync();
                TempData["Error"] = "El producto tiene movimientos registrados y no puede eliminarse. Fue desactivado en su lugar.";
                return RedirectToAction(nameof(Index));
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Producto eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}