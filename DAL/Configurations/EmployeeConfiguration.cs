using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.EmployeeId).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SecondName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Position).HasMaxLength(50);
            builder.Property(x => x.Gender).HasMaxLength(50);
            builder.Property(x => x.Division).IsRequired();
            builder.Property(x => x.BirthDay).IsRequired();


            builder.HasData(
                new Employee()
                {
                    EmployeeId = 1,
                    FirstName = "Валерий",
                    SecondName = "Жмышенко",
                    LastName = "Альбертович",
                    Position = "General",
                    BirthDay = DateTime.UtcNow
                },
                new Employee()
                {
                    EmployeeId = 2,
                    FirstName = "Михаил",
                    SecondName = "Зубенко",
                    LastName = "Петрович",
                    Position = "Maffiosnic",
                    BirthDay = DateTime.UtcNow
                }); 


        }
    }
}
