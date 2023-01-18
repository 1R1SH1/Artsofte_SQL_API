using Artsofte.Core;
using Artsofte.Models;
using Artsofte.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Artsofte.Controllers
{
    public class HomeController : Controller
    {
        MultipleClass obj = new MultipleClass();
        public IActionResult Index()
        {
            var emp = JsonConvert.DeserializeObject<List<Employee>>(CRUD.Read("Employee"));
            return View(emp);
        }

        public IActionResult Add()
        {
            obj.Employees = JsonConvert.DeserializeObject<List<Employee>>(CRUD.Read("Employee"));
            obj.Departments = JsonConvert.DeserializeObject<List<Department>>(CRUD.Read("Department"));
            obj.ProgrammingLanguages = JsonConvert.DeserializeObject<List<ProgrammingLanguage>>(CRUD.Read("ProgrammingLanguage"));
            return View(obj);
        }

        public IActionResult AddEmployee(Employee data)
        {
            Employee employee = new Employee();
            employee.Name = data.Name;
            employee.SurName = data.SurName;
            employee.Age = data.Age;
            employee.Gender = data.Gender;
            employee.Departments = data.Departments;
            employee.ProgrammingLanguages = data.ProgrammingLanguages;
            CRUD.Create("Employee/add", JsonConvert.SerializeObject(employee));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteEmployee(string id)
        {
            CRUD.Delete($"Employee/{id}");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit()
        {
            obj.Employees = JsonConvert.DeserializeObject<List<Employee>>(CRUD.Read("Employee"));
            obj.Departments = JsonConvert.DeserializeObject<List<Department>>(CRUD.Read("Department"));
            obj.ProgrammingLanguages = JsonConvert.DeserializeObject<List<ProgrammingLanguage>>(CRUD.Read("ProgrammingLanguage"));
            return View(obj);
        }

        public IActionResult EditEmployee(Employee data)
        {
            CRUD.Update("Employee/edit", JsonConvert.SerializeObject(data));

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}