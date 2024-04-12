﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SecondName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DriverLicense);
            builder.Property(x => x.Position).HasMaxLength(50);
            builder.Property(x => x.Gender).HasMaxLength(50);

            builder.HasData(new Employee()
            {
                FirstName = "TestEmolyeFirstName",
                SecondName = "TestEmolyeSecondName",
                LastName = "TestEmolyeLastName",
                DriverLicense = true,

            });


        }
    }
}
