using EmployeeCrud.Domain;
using EmployeeCrud.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCrud.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public IEmployeeService _employeeService { get; }
        public ILogger<EmployeesController> _logger { get; }

        public EmployeesController(
           IEmployeeService employeeService,
           ILogger<EmployeesController> logger) : base()
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var foundList = await _employeeService.GetAll(cancellationToken);
            return Ok(foundList);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var emp = await _employeeService.Get(id, cancellationToken);
            if (emp == null) return BadRequest("Employee not found");
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee newEmp, CancellationToken cancellationToken = default)
        {
            var emp = await _employeeService.Create(newEmp, cancellationToken);
            if (emp == null) return BadRequest("Employee creation failed");
            return Ok(emp);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Employee updatedEmp,  CancellationToken cancellationToken = default)
        {
            var isOk = await _employeeService.Update(id, updatedEmp , cancellationToken);
            return isOk ? Ok(id) : BadRequest("Employee update failed");
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var isOk = await _employeeService.Delete(id,cancellationToken);
            return isOk ? Ok(id) : BadRequest("Employee deletion failed");
        }
    }
}
