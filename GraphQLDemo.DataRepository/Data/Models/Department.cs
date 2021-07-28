using Opticient.EFCore.Repository;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo.DataRepository.Data.Models
{
    public partial class Department : IDbEntity<int>
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
