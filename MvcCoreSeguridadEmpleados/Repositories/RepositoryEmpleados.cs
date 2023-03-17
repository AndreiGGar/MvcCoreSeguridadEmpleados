using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreSeguridadEmpleados.Context;
using MvcCoreSeguridadEmpleados.Models;

namespace MvcCoreSeguridadEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private DataContext context;

        public RepositoryEmpleados(DataContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public Empleado FindEmpleado(int idempleado)
        {
            return this.context.Empleados.FirstOrDefault(x => x.IdEmpleado == idempleado);
        }

        public List<Empleado> FindEmpleadosDept(int deptno)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.Dept_no == deptno
                           select datos;
            return consulta.ToList();
        }

        public async Task AumentarSalarioAsync(int deptno, int salario)
        {
            string sql = "SP_AUMENTAR_SALARIOS_DEPT @DEPT_NO, @SALARIO";
            SqlParameter pamdeptno = new SqlParameter("@DEPT_NO", deptno);
            SqlParameter pamsalario = new SqlParameter("@SALARIO", salario);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamdeptno, pamsalario);
        }

        public async Task<Empleado> ExisteEmpleado(string apellido, int idempleado)
        {
            var consulta = this.context.Empleados.Where(x => x.Apellido == apellido && x.IdEmpleado == idempleado);
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
