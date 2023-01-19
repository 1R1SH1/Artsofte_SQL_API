using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artsofte.Models.Classes
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int Floor { get; set; }
        public List<SelectListItem> DepartmentsList { get; set; }
    }
}
