using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace Artsofte.Models.Classes
{
    public class MultipleClass
    {
        public Employee Employee { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem>DepartmentList { get; set; }
        public List<SelectListItem> ProgrammingLanguageList { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
        public List<ProgrammingLanguage> ProgrammingLanguages { get; set;}
    }
}
