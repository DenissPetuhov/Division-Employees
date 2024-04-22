using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.SecondName).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Position).HasMaxLength(50);
            builder.Property(x => x.Gender).HasMaxLength(50);
            builder.Property(x => x.BirthDay).IsRequired();
            builder.Property(x => x.DivisionId).IsRequired();

            builder.HasOne(x => x.Division)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DivisionId)
                .IsRequired();
          
            builder.HasData(
                new Employee()
                {
             
                    Id = 1,
                    FirstName = "Валерий",
                    SecondName = "Жмышенко",
                    LastName = "Альбертович",
                    Position = "General",
                    BirthDay = DateTime.UtcNow,
                    DriverLicense = true,
                    DivisionId = 1
           
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Михаил",
                    SecondName = "Зубенко",
                    LastName = "Петрович",
                    Position = "Maffiosnic",
                    BirthDay = DateTime.UtcNow,
                    DriverLicense = true,
                    DivisionId = 1

                });


        }
    }
}
