// <auto-generated />
using System;
using GraphQLDemo.DataRepository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQLDemo.DataRepository.Migrations
{
    [DbContext(typeof(GraphQLDemoContext))]
    [Migration("20210723223059_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Latin1_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraphQLDemo.DataRepository.Data.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Computer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Account"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Engineering"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Human Resource"
                        });
                });

            modelBuilder.Entity("GraphQLDemo.DataRepository.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Bonus")
                        .HasColumnType("numeric(6,2)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DepartmentId" }, "IX_Employee_DepartmentId");

                    b.HasIndex(new[] { "ManagerId" }, "IX_Employee_ManagerId");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bonus = 400m,
                            DepartmentId = 3,
                            Name = "John",
                            Salary = 25000
                        },
                        new
                        {
                            Id = 2,
                            Bonus = 800m,
                            DepartmentId = 3,
                            ManagerId = 1,
                            Name = "Robert",
                            Salary = 15000
                        },
                        new
                        {
                            Id = 3,
                            Bonus = 175m,
                            DepartmentId = 2,
                            ManagerId = 2,
                            Name = "Richard",
                            Salary = 10000
                        },
                        new
                        {
                            Id = 4,
                            Bonus = 0m,
                            DepartmentId = 2,
                            ManagerId = 3,
                            Name = "Mark",
                            Salary = 7500
                        },
                        new
                        {
                            Id = 5,
                            Bonus = 0m,
                            DepartmentId = 1,
                            ManagerId = 2,
                            Name = "Stefan",
                            Salary = 5000
                        });
                });

            modelBuilder.Entity("GraphQLDemo.DataRepository.Data.Models.Employee", b =>
                {
                    b.HasOne("GraphQLDemo.DataRepository.Data.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Employee_Department")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQLDemo.DataRepository.Data.Models.Employee", "Manager")
                        .WithMany("ManagedEmployees")
                        .HasForeignKey("ManagerId")
                        .HasConstraintName("FK_Employee_Employee")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Department");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("GraphQLDemo.DataRepository.Data.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("GraphQLDemo.DataRepository.Data.Models.Employee", b =>
                {
                    b.Navigation("ManagedEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
