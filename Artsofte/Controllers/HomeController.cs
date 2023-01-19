using Artsofte.Core;
using Artsofte.Models;
using Artsofte.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Artsofte.Controllers
{
    public class HomeController : Controller
    {
        private MultipleClass obj = new();
        public IActionResult Index()
        {
            obj.Employees = JsonConvert.DeserializeObject<List<Employee>>(CRUD.Read("Employee"));
            return View(obj);
        }

        public IActionResult Add()
        {
            var department = JsonConvert.DeserializeObject<IEnumerable<Department>>(CRUD.Read("Department")).OrderBy(d => d.DepartmentName).Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.DepartmentName}).ToList();
            var language = JsonConvert.DeserializeObject<IEnumerable<ProgrammingLanguage>>(CRUD.Read("ProgrammingLanguage")).OrderBy(l => l.LanguageName).Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.LanguageName }).ToList();
            obj.Employees = JsonConvert.DeserializeObject<List<Employee>>(CRUD.Read("Employee"));
            obj.DepartmentList = department;
            obj.ProgrammingLanguageList = language;
            return View(obj);
        }

        public IActionResult AddEmployee(Employee employee)
        {
            CRUD.Create("Employee/add", JsonConvert.SerializeObject(employee));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        public IActionResult AddDepartments(Department department)
        {
            CRUD.Create("Department/add", JsonConvert.SerializeObject(department));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddLanguage()
        {
            return View();
        }

        public IActionResult AddLanguages(ProgrammingLanguage language)
        {
            CRUD.Create("ProgrammingLanguage/add", JsonConvert.SerializeObject(language));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteEmployee(int id)
        {
            CRUD.Delete($"Employee/{id}");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit()
        {
            var department = JsonConvert.DeserializeObject<IEnumerable<Department>>(CRUD.Read("Department")).OrderBy(d => d.DepartmentName).Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.DepartmentName }).ToList();
            var language = JsonConvert.DeserializeObject<IEnumerable<ProgrammingLanguage>>(CRUD.Read("ProgrammingLanguage")).OrderBy(l => l.LanguageName).Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.LanguageName }).ToList();
            var employee = JsonConvert.DeserializeObject<IEnumerable<Employee>>(CRUD.Read("Department")).OrderBy(e => e.Id).Select(e => new SelectListItem { Value = e.Id.ToString() }).ToList();
            
            obj.EmployeeList= employee;
            obj.DepartmentList = department;
            obj.ProgrammingLanguageList = language;
            return View(obj);
        }

        public IActionResult EditEmployee(Employee data)
        {
            CRUD.Update($"Employee/edit", JsonConvert.SerializeObject(data));
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}