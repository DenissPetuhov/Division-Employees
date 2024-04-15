using Domain.Entity;

namespace Domain.Dto
{

    public record class DivisionDto(int DivisionId, string Name, DateTime DateCreate, string Discription);

    //{
    //    public int DivisionId { get; set; }
    //    public string Name { get; set; }
    //    public DateTime DateCreate { get; set; }
    //    public string Discription { get; set; }
    //    public List<Employee> Employees { get; set; }

    //}
}
