using EmployeeMangment.Models;
using EmployeeMangment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Controllers
{
     // [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IEmployeeReposiotry _employeeRepository;
        private readonly IHostingEnvironment hostingEnviroment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeReposiotry employeeRepository,
                              IHostingEnvironment hostingEnviroment,
                              ILogger<HomeController> logger)
        {

            _employeeRepository = employeeRepository;
            this.hostingEnviroment = hostingEnviroment;
            this.logger = logger;
        }

        //[Route("~/home")]
        //[Route("~/")]
        //[Route("[action]")]
        [AllowAnonymous]
        public ViewResult index()
        {
            IEnumerable<Employee> empModel = _employeeRepository.GetAllEmployee();
            ViewBag.pageTitle = "Index";
            return View(empModel);
        }

        //  [Route("{id?}")]
       [AllowAnonymous]
        public ViewResult details(int? id)
        {
            logger.LogTrace("Log Trace");
            logger.LogDebug("Log Debug");
            logger.LogInformation("Log Information");
            logger.LogWarning("Log Warning");
            logger.LogError("Log Error");
            logger.LogCritical("Log Critical");



            // LogLevel
            // throw new Exception("Error in Details View");

            Employee employees = _employeeRepository.GetEmployee(id.Value);

            if(employees == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }




            //return "id = " + id.Value.ToString() + "My Name = " + name;
            Employee employee = employees;

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                emp = employee,
                PageTitle = "New  Details.cshtml"
            };




            //ViewData["Emp"] = employee;
            //ViewData["PageTitle"] = "Details.csHtml";

            //ViewBag.Emp = employee;
            //ViewBag.PageTitles = "Details.csHtml";

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();

        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string unqueFileName = ProcessUploadedFile(model);

                Employee emp = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = unqueFileName
                };

                _employeeRepository.AddEmploye(emp);
            return RedirectToAction("details", new { id = emp.Id });
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel empEdit = new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(empEdit);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;


                if(model.Photo!=null)
                { 
                    if(model.ExistingPhotoPath!=null)
                    {
                       string path= Path.Combine(hostingEnviroment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(path);
                    }

                    employee.PhotoPath = ProcessUploadedFile(model);
                }


                

                _employeeRepository.upadateEmp(employee);
                return RedirectToAction("index");
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string unqueFileName = null;
            if (model.Photo != null)
            {
                String uploadFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                unqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, unqueFileName);

                using (FileStream file = new FileStream(filePath, FileMode.Create))
                { 

                    model.Photo.CopyTo(file);
                }
            }

            return unqueFileName;
        }
    }
}
 