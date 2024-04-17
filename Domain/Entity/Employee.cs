namespace Domain.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? Position { get; set; }
        public bool? DriverLicense { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }

    }
}
