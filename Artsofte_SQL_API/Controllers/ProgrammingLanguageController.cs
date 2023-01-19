using Artsofte_SQL_API.Models.Classes;
using Artsofte_SQL_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte_SQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguageController : Controller
    {
        private readonly PLanguageCore _core;
        public ProgrammingLanguageController(PLanguageCore core)
        {
            _core = core;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] ProgrammingLanguage data)
        {
            await _core.PostLanguage(data);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ProgrammingLanguage>> Get()
        {
            return await _core.GetLanguage();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProgrammingLanguage data)
        {
            await _core.PutLanguage(data);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _core.DeleteLanguage(id);
            return Ok();
        }
    }
}
