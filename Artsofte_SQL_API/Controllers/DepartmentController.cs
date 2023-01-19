using Artsofte_SQL_API.Models.Classes;
using Artsofte_SQL_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte_SQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentCore _core;
        public DepartmentController(DepartmentCore core)
        {
            _core = core;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Department data)
        {
            await _core.PostDepartment(data);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _core.GetDepartments();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Department data)
        {
            await _core.PutDepartment(data);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _core.DeleteDepartment(id);
            return Ok();
        }
    }
}
