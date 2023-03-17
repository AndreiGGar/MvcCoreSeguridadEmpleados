using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSeguridadEmpleados.Models;
using MvcCoreSeguridadEmpleados.Repositories;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace MvcCoreSeguridadEmpleados.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryEmpleados repo;

        public ManagedController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            Empleado empleado = await this.repo.ExisteEmpleado(username, int.Parse(password));
            if (empleado != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, username);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, empleado.IdEmpleado.ToString());
                identity.AddClaim(claimId);
                Claim claimOficio = new Claim(ClaimTypes.Role, empleado.Oficio);
                identity.AddClaim(claimOficio);
                Claim claimSalario = new Claim("Salario", empleado.Salario.ToString());
                identity.AddClaim(claimSalario);
                Claim claimDepartamento = new Claim("Departamento", empleado.Dept_no.ToString());
                identity.AddClaim(claimDepartamento);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                return RedirectToAction("PerfilEmpleado", "Empleados");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
