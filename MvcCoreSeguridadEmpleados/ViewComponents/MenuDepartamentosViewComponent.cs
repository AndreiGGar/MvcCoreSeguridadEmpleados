using Microsoft.AspNetCore.Mvc;
using MvcCoreSeguridadEmpleados.Repositories;
using MvcCoreSeguridadEmpleados.Models;

namespace MvcCoreSeguridadEmpleados.ViewComponents
{
    public class MenuDepartamentosViewComponent: ViewComponent
    {
        private RepositoryDepartamentos repo;

        public MenuDepartamentosViewComponent(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }
    }
}
