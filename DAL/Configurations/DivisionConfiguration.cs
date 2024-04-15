using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Configurations
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.Property(x => x.DivisionId).ValueGeneratedOnAdd();
            builder.Property(x => x.Discription).HasMaxLength(150);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Division)
                .HasForeignKey(x => x.EmployeeId)
                .HasPrincipalKey(x => x.DivisionId);

            builder.HasData(new Division()
            {
                DivisionId = 1,
                Name = "OPG#1",
                Discription = "Maffia",
                Employees = new List<Employee> {
                    new Employee 
                    {
                        EmployeeId = 1
                    },
                    new Employee 
                    {
                        EmployeeId = 2
                    }
                }


            });

        }
    }
}
