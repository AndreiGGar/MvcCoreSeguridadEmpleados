using Microsoft.EntityFrameworkCore;
using MvcCoreSeguridadEmpleados.Models;

namespace MvcCoreSeguridadEmpleados.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

    }
}
