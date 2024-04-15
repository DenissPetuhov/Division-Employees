using Domain.Entity;

namespace Domain.Dto
{
    public record class EmployeeDto(int EmployeeId, string FirstName, string SecondName,
        string LastName, DateTime BirthDay, string Gender, string Position, bool DriverLicense, int divisionId);

    public record class CreateEmployeeDto(string FirstName, string SecondName,
       string LastName, DateTime BirthDay, int divisionId);
    //{
    //    public int EmployeeId { get; set; }
    //    public string FirstName { get; set; }
    //    public string SecondName { get; set; }
    //    public string LastName { get; set; }
    //    public DateTime BirthDay { get; set; }
    //    public string Gender { get; set; }
    //    public string Position { get; set; }
    //    public bool DriverLicense { get; set; }
    //    public Division Division { get; set; }
    //}
}
