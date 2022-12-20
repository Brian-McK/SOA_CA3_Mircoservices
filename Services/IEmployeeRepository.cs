using BrianMcKenna_SOA_CA3.Entities;

namespace BrianMcKenna_SOA_CA3.Services;

public interface IEmployeeRepository: IDisposable
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(Guid id);
    Task InsertEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(Guid id);
    Task UpdateEmployeeAsync(Employee employee);
    bool EmployeeExists(Guid id);
}