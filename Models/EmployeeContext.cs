using Microsoft.EntityFrameworkCore;

namespace BrianMcKenna_SOA_CA3.Models;

public class EmployeeContext: DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> UserItems { get; set; } = null!;
}