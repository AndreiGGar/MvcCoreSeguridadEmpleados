using Microsoft.AspNetCore.Mvc;
using MvcCoreSeguridadEmpleados.Filters;
using MvcCoreSeguridadEmpleados.Models;
using MvcCoreSeguridadEmpleados.Repositories;

namespace MvcCoreSeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }

        public IActionResult EmpleadosDept(int deptno)
        {
            List<Empleado> empleados = this.repo.FindEmpleadosDept(deptno);
            ViewData["DEPTNO"] = deptno;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> EmpleadosDept(int deptno, int salario)
        {
            await this.repo.AumentarSalarioAsync(deptno, salario);
            List<Empleado> empleados = this.repo.FindEmpleadosDept(deptno);
            ViewData["DEPTNO"] = deptno;
            return View(empleados);
        }

        [AuthorizeEmpleados]
        public IActionResult PerfilEmpleado()
        {
            return View();
        }
    }
}
