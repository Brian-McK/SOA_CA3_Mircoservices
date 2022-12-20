using BrianMcKenna_SOA_CA3.Entities;
using BrianMcKenna_SOA_CA3.Models;
using Microsoft.EntityFrameworkCore;

namespace BrianMcKenna_SOA_CA3.Services;

public class EmployeeRepository: IEmployeeRepository, IDisposable
{
    private readonly EmployeeContext _employeeContext;

    public EmployeeRepository(EmployeeContext employeeContext)
    {
        _employeeContext = employeeContext;
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeContext.Employees.ToListAsync();
    }
    
    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeContext.Employees.FindAsync(id);

        return employee;
    }
    
    public async Task InsertEmployeeAsync(Employee employee)
    {
        await _employeeContext.Employees.AddAsync(employee);

        await SaveAsync();
    }
    
    public async Task DeleteEmployeeAsync(Guid id)
    {
        var employee = await _employeeContext.Employees.FindAsync(id);

        if (employee != null) _employeeContext.Employees.Remove(employee);

        await SaveAsync();
    }
    
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _employeeContext.Entry(employee).State = EntityState.Modified;
        
        try
        {
            await SaveAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!EmployeeExists(employee.Id))
            {
                throw new Exception(exception.Message);
            }
        }
    }

    private async Task SaveAsync()
    {
        await _employeeContext.SaveChangesAsync();
    }

    private bool _disposed = false;
    
    private async Task Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                await _employeeContext.DisposeAsync();
            }
        }
        _disposed = true;
    }

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool EmployeeExists(Guid id)
    {
        return (_employeeContext.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}