using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Models
{
    public interface IEmployeeReposiotry
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployee();
        Employee AddEmploye(Employee employee);
        Employee DeleteEmp(int id);
        Employee upadateEmp(Employee uptEmp);
    }


}
