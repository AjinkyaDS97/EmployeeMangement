using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Controllers
{
    public class DepartmentController : Controller
    {
        public string ListN()
        {
            return "Department List() Method";
        }

        public string DetailsN()
        {
            return "Department DetailsN() Method";
        }
    }
}
