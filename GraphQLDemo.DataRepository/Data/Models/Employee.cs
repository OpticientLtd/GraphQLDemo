using Opticient.EFCore.Repository;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo.DataRepository.Data.Models
{
    public partial class Employee : IDbEntity<int>
    {
        public Employee()
        {
            ManagedEmployees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public int Salary { get; set; }
        public decimal? Bonus { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> ManagedEmployees { get; set; }
    }
}
