using AutoMapper;
using BrianMcKenna_SOA_CA3.Entities;
using BrianMcKenna_SOA_CA3.Models;

namespace BrianMcKenna_SOA_CA3.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.Surname}"))
            .ForMember(dest => dest.Dob,
                opt => opt.MapFrom(src =>
                    $"{src.DateOfBirth:dd/MM/yyyy}"));

        CreateMap<Employee, EmployeeForUpdatingDto>().ForMember(
            dest => dest.Dob,
            opt => opt.MapFrom(src => $"{src.DateOfBirth:dd/MM/yyyy}"));

        CreateMap<EmployeeForUpdatingDto, Employee>().ForMember(
            dest => dest.DateOfBirth,
            opt => opt.MapFrom(src =>
                $"{ConvertDateStringToDateTime(src.Dob)}"));

        CreateMap<EmployeeForUpdatingDto, EmployeeForCreatingDto>();
    }

    private static DateTime? ConvertDateStringToDateTime(string? dateStr)
    {
        if (dateStr == null) return null;

        var date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", null);

        return date;
    }
}