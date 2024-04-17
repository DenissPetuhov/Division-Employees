namespace Domain.Entity
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public string? Discription { get; set; }
        public int? ParentDivisionId { get; set; }
        public Division? ParentDivision { get; set; }
        public List<Division> Divisions { get; set; } = new List<Division> { };
        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();

    }
}
