namespace Domain.Entity
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public string Discription { get; set; }
        public List<Employee>? Employees { get; set; }

    }
}
