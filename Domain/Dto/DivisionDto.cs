using Domain.Entity;

namespace Domain.Dto
{

    public record class DivisionDto(int Id, string Name, string? Description, int ParentDivisionId);
    public record class CreateDivisionDto(string Name, string? Description);
    public record class AddParentDivisionDto(int Id, int ParentDivisionId);

    

//    public int Id { get; set; }
//    public string Name { get; set; }
//    public DateTime DateCreate { get; set; }

//    public string? Discription { get; set; }
//    public int? ParentDivisionId { get; set; }
//    public Division? ParentDivision { get; set; }
//    public List<Division> Divisions { get; set; } = new List<Division> { };
//    public ICollection<Employee>? Employees { get; set; } = new List<Employee>();

}
