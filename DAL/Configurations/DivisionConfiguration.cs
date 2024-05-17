using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Configurations
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description).HasMaxLength(150);
            builder.Property(x => x.Name).HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.ParentDivision)
                .WithMany(x => x.Divisions)
                .HasForeignKey(x => x.ParentDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Navigation(x => x.Employees).AutoInclude();

            builder.HasData(
                new Division()
                {
                    Id = 1,
                    Name = "OPG#1",
                    Description = "Maffia"
                });
        }
    }
}
