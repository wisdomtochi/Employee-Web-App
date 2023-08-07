using EmployeeWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
            //return (IActionResult)_employeeRepository.GetEmployee(1);
        }

        [HttpPost]
        public IActionResult Index(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = new()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                };

                var employed = _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = employed.Id });
            }
            return View();
        }

        public ViewResult Details(int? id)
        {
            var model = _employeeRepository.GetEmployee(id ?? 1);
            ViewBag.PageTitle = "Employee Details";
            return View(model);
        }
    }
}
