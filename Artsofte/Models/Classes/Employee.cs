using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;

namespace Artsofte.Models.Classes
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public int LanguageId { get; set; }
        public Department Departments { get; set; }
        public ProgrammingLanguage ProgrammingLanguages { get; set; }
        public List<SelectListItem> EmployeesList { get; set; }
    }
}
