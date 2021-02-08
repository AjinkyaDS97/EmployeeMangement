using EmployeeMangment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Models
{
    public class MockEmployeeRepository : IEmployeeReposiotry
    {

        private List<Employee> emplist;


        public MockEmployeeRepository()
        {
            emplist = new List<Employee>()
            {
            new Employee { Id = 1, Name = "Mary", Email = "Marray@gmail.com", Department = Dept.CTO },
            new Employee { Id = 2, Name = "Amy", Email = "Amy@gmail.com", Department = Dept.IT },
            new Employee { Id = 3, Name = "Jessy", Email = "Jessy@gmail.com", Department = Dept.PayRoll },
            new Employee { Id = 4, Name = "kemy", Email = "Kemy@gmail.com", Department =Dept.IT
            }
           };
        }

        public Employee AddEmploye(Employee employee)
        {
            employee.Id = emplist.Max(e => e.Id) + 1;
            emplist.Add(employee);
            return employee;
        }

        public Employee DeleteEmp(int id)
        {
           Employee emp= emplist.FirstOrDefault(e => e.Id == id);
            if(emp!=null)
            {
                emplist.Remove(emp);
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return emplist;
        }

        public Employee GetEmployee(int Id)
        {
            return emplist.FirstOrDefault(e => e.Id == Id);
        }

        public Employee upadateEmp(Employee uptEmp)
        {
            Employee emp = emplist.FirstOrDefault(e => e.Id == uptEmp.Id);
            if (emp != null)
            {
                emp.Id = uptEmp.Id;
                emp.Name = uptEmp.Name;
                emp.Email = uptEmp.Email;
                emp.Department = uptEmp.Department;
            }
            return emp;
        }
    }

       
     
}
