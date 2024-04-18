﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentDivisionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentDivisionId");

                    b.ToTable("Division");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Maffia",
                            Name = "OPG#1"
                        });
                });

            modelBuilder.Entity("Domain.Entity.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<bool?>("DriverLicense")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDay = new DateTime(2024, 4, 18, 20, 41, 41, 872, DateTimeKind.Utc).AddTicks(5412),
                            DivisionId = 1,
                            DriverLicense = true,
                            FirstName = "Валерий",
                            LastName = "Альбертович",
                            Position = "General",
                            SecondName = "Жмышенко"
                        },
                        new
                        {
                            Id = 2,
                            BirthDay = new DateTime(2024, 4, 18, 20, 41, 41, 872, DateTimeKind.Utc).AddTicks(5416),
                            DivisionId = 1,
                            DriverLicense = true,
                            FirstName = "Михаил",
                            LastName = "Петрович",
                            Position = "Maffiosnic",
                            SecondName = "Зубенко"
                        });
                });

            modelBuilder.Entity("Domain.Entity.Division", b =>
                {
                    b.HasOne("Domain.Entity.Division", "ParentDivision")
                        .WithMany("Divisions")
                        .HasForeignKey("ParentDivisionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ParentDivision");
                });

            modelBuilder.Entity("Domain.Entity.Employee", b =>
                {
                    b.HasOne("Domain.Entity.Division", "Division")
                        .WithMany("Employees")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Domain.Entity.Division", b =>
                {
                    b.Navigation("Divisions");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
