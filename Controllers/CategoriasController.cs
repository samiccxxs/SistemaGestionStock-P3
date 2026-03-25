using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStock.Filters;
using SistemaStock.Models;

namespace SistemaStock.Controllers
{
    [SessionAuthFilter(1)]
    public class CategoriasController : Controller
    {
        private readonly SistemaStockContext _context;

        public CategoriasController(SistemaStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var existe = await _context.Categorias
                    .AnyAsync(c => c.Nombre == categoria.Nombre);

                if (existe)
                {
                    TempData["Error"] = "Ya existe una categoría con ese nombre.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Categoría creada correctamente.";
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
        public async Task<IActionResult> Edit([Bind("CategoriaId,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nombreExiste = await _context.Categorias
                        .AnyAsync(c => c.Nombre == categoria.Nombre && c.CategoriaId != categoria.CategoriaId);

                    if (nombreExiste)
                    {
                        TempData["Error"] = "Ya existe otra categoría con ese nombre.";
                        return RedirectToAction(nameof(Index));
                    }

                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Categoría actualizada correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categorias.Any(c => c.CategoriaId == categoria.CategoriaId))
                        TempData["Error"] = "La categoría no existe.";
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tieneProductos = await _context.Productos
                .AnyAsync(p => p.CategoriaId == id);

            if (tieneProductos)
            {
                TempData["Error"] = "No puedes eliminar esta categoría porque tiene productos asociados.";
                return RedirectToAction(nameof(Index));
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Categoría eliminada correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriaId == id);
        }
    }
}