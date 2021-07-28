using GraphQLDemo.DataRepository.Data.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GraphQLDemo.DataRepository.Data
{
    public partial class GraphQLDemoContext : DbContext
    {
        public GraphQLDemoContext(DbContextOptions<GraphQLDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.HasData(InitialData.InitialDepartments);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.DepartmentId, "IX_Employee_DepartmentId");

                entity.HasIndex(e => e.ManagerId, "IX_Employee_ManagerId");

                entity.Property(e => e.Bonus).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ManagedEmployees)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Employee_Employee");
                entity.HasData(InitialData.InitialEmployees);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
