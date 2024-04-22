using Domain.Entity;

namespace Domain.Dto
{
    public record class EmployeeDto(int Id, string FirstName, string SecondName,
        string LastName, DateTime BirthDay, string? Gender, string? Position, bool? DriverLicense);

    public record class CreateEmployeeDto(string FirstName, string SecondName,
        string LastName, DateTime BirthDay, string? Gender, string? Position, bool? DriverLicense, int divisionId);

 
}
