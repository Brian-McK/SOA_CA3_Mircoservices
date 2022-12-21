using AutoMapper;
using BrianMcKenna_SOA_CA3.Entities;
using BrianMcKenna_SOA_CA3.Models;
using BrianMcKenna_SOA_CA3.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrianMcKenna_SOA_CA3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    // GET: api/Employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var items = await _employeeRepository.GetAllEmployeesAsync();

        if (!items.Any()) return NotFound();

        var employeesList = _mapper.Map<IEnumerable<EmployeeDto>>(items);

        return Ok(employeesList);
    }


    // GET: api/Employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(Guid id)
    {
        var item = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (item == null) return NotFound();

        var employee = _mapper.Map<EmployeeDto>(item);

        return Ok(employee);
    }

    // PUT: api/Employees/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeForUpdatingDto employeeForUpdating)
    {
        if (!_employeeRepository.EmployeeExists(id)) return BadRequest();

        var employeeEntity = _mapper.Map<Employee>(employeeForUpdating);

        employeeEntity.Id = id;

        await _employeeRepository.UpdateEmployeeAsync(employeeEntity);

        return Ok(employeeForUpdating);
    }

    // POST: api/Employees
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeForCreatingDto employeeForCreating)
    {
        var employeeEntity = _mapper.Map<Employee>(employeeForCreating);

        await _employeeRepository.InsertEmployeeAsync(employeeEntity);

        return CreatedAtAction(nameof(GetEmployees), new { id = employeeEntity.Id }, employeeEntity);
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);

        return NoContent();
    }
}