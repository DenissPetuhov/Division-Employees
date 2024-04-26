using Domain.Entity;

namespace Domain.Dto
{
    public record class DivisionDto(int Id, string Name, string? Description, int? ParentDivisionId);
    public record class DivisionDtoTree(int Id, string Name, ICollection<DivisionDtoTree> Divisions);
    public record class CreateDivisionDto(string Name, string? Description);
    public record class AddParentDivisionDto(int Id, int ParentDivisionId);


}
