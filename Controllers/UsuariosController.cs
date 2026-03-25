using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStock.Filters;
using SistemaStock.Models;

namespace SistemaStock.Controllers
{
    [SessionAuthFilter(1)]
    public class UsuariosController : Controller
    {
        private readonly SistemaStockContext _context;

        public UsuariosController(SistemaStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();
            var usuarios = await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreUsuario,Password,Nombre,RolId,Activo")] Usuarios usuario)
        {
            ModelState.Remove("Rol");

            if (ModelState.IsValid)
            {
                var usuarioExiste = await _context.Usuarios
                    .AnyAsync(u => u.NombreUsuario == usuario.NombreUsuario);

                if (usuarioExiste)
                {
                    TempData["Error"] = "El nombre de usuario ya existe.";
                    return RedirectToAction(nameof(Index));
                }

                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario creado correctamente.";
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
        public async Task<IActionResult> Edit([Bind("UsuarioId,NombreUsuario,Password,Nombre,RolId,Activo")] Usuarios usuario)
        {
            ModelState.Remove("Rol");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    var original = await _context.Usuarios
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.UsuarioId == usuario.UsuarioId);

                    if (original == null)
                    {
                        TempData["Error"] = "El usuario no existe.";
                        return RedirectToAction(nameof(Index));
                    }

                    var nombreExiste = await _context.Usuarios
                        .AnyAsync(u => u.NombreUsuario == usuario.NombreUsuario && u.UsuarioId != usuario.UsuarioId);

                    if (nombreExiste)
                    {
                        TempData["Error"] = "El nombre de usuario ya está en uso.";
                        return RedirectToAction(nameof(Index));
                    }

                    if (string.IsNullOrWhiteSpace(usuario.Password))
                        usuario.Password = original.Password;
                    else
                        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Usuario actualizado correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuarios.Any(u => u.UsuarioId == usuario.UsuarioId))
                        TempData["Error"] = "El usuario no existe.";
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
        public async Task<IActionResult> DeleteConfirmed(int UsuarioId)
        {
            var sesionId = HttpContext.Session.GetString("UsuarioId");
            if (sesionId == UsuarioId.ToString())
            {
                TempData["Error"] = "No puedes eliminar tu propio usuario.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios.FindAsync(UsuarioId);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario eliminado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}