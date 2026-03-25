using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStock.Filters;
using SistemaStock.Models;

namespace SistemaStock.Controllers
{
    [SessionAuthFilter]
    public class HomeController : Controller
    {
        private readonly SistemaStockContext _context;

        public HomeController(SistemaStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProductos = await _context.Productos.CountAsync();
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();
            ViewBag.TotalUsuarios = await _context.Usuarios.CountAsync();
            ViewBag.ProductosSinStock = await _context.Productos.CountAsync(p => p.Stock == 0);
            ViewBag.ProductosBajoStock = await _context.Productos.CountAsync(p => p.Stock > 0 && p.Stock <= p.StockMinimo);

            ViewBag.ListaBajoStock = await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Stock <= p.StockMinimo)
                .OrderBy(p => p.Stock)
                .ToListAsync();

            ViewBag.UltimosProductos = await _context.Productos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.FechaCreacion)
                .Take(10)
                .ToListAsync();

            ViewBag.NombreUsuario = HttpContext.Session.GetString("Nombre");
            ViewBag.RolId = HttpContext.Session.GetString("RolId");

            return View();
        }
    }
}