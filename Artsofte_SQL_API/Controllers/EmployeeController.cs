using Artsofte_SQL_API.Models.Classes;
using Artsofte_SQL_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte_SQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeCore _core;
        public EmployeeController(EmployeeCore core)
        {
            _core = core;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Employee data)
        {
            await _core.PostEmployee(data);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _core.GetEmployees();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Employee data)
        {
            await _core.PutEmployee(data);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _core.DeleteEmployee(id);
            return Ok();
        }
    }
}
