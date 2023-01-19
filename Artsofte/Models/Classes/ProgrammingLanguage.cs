using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artsofte.Models.Classes
{
    public class ProgrammingLanguage
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public List<SelectListItem> LanguagesList { get; set; }
    }
}
