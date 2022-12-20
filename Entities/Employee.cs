namespace BrianMcKenna_SOA_CA3.Models;

public class Employee
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? Surname { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}