using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Models
{
    public class SQLEmployeeRepository : IEmployeeReposiotry
    {
        private readonly AppDBContext context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        public SQLEmployeeRepository(AppDBContext context,
                                     ILogger<SQLEmployeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Employee AddEmploye(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmp(int id)
        {
            var emp=context.Employees.Find(id);
            if(emp!=null)
            {
                context.Remove(emp);
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            logger.LogTrace("Log Trace");
            logger.LogDebug("Log Debug");
            logger.LogInformation("Log Information");
            logger.LogWarning("Log Warning");
            logger.LogError("Log Error");
            logger.LogCritical("Log Critical");
            return context.Employees.Find(Id);
        }

        public Employee upadateEmp(Employee uptEmp)
        {
           var emp= context.Employees.Attach(uptEmp);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return uptEmp;

        }
    }
}
