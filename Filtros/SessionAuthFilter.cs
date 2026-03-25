using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SistemaStock.Filters
{
    public class SessionAuthFilter : ActionFilterAttribute
    {
        private readonly int _rolRequerido; // 2 = Usuario, 1 = Admin

        public SessionAuthFilter(int rolRequerido = 2)
        {
            _rolRequerido = rolRequerido;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuarioId = context.HttpContext.Session.GetString("UsuarioId");

            // Si no hay sesión, redirige al login
            if (string.IsNullOrEmpty(usuarioId))
            {
                context.Result = new RedirectToActionResult("Login", "Cuenta", null);
                return;
            }

            // Si se requiere Admin (RolId = 1)
            if (_rolRequerido == 1)
            {
                var rolId = context.HttpContext.Session.GetString("RolId");
                if (rolId != "1")
                {
                    context.Result = new RedirectToActionResult("Denegado", "Cuenta", null);
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}