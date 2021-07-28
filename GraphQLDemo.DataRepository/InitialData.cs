using GraphQLDemo.DataRepository.Data.Models;
using System.Collections.Generic;

namespace GraphQLDemo.DataRepository
{
    public static class InitialData
    {
        public static IEnumerable<Department> InitialDepartments => new[]
                {
                    new Department{ Id = 1, Name = "Computer"},
                    new Department{ Id = 2, Name = "Account"},
                    new Department{ Id = 3, Name = "Engineering"},
                    new Department{ Id = 4, Name = "Human Resource"}
                };

        public static IEnumerable<Employee> InitialEmployees => new[] {
            new Employee {Id = 1,Name="John",DepartmentId=3,ManagerId=null,Salary=25000,Bonus=400M},
            new Employee {Id = 2,Name="Robert",DepartmentId=3,ManagerId=1,Salary=15000,Bonus=800M},
            new Employee {Id = 3,Name="Richard",DepartmentId=2,ManagerId=2,Salary=10000,Bonus=175M},
            new Employee {Id = 4,Name="Mark",DepartmentId=2,ManagerId=3,Salary=7500,Bonus=0M},
            new Employee {Id = 5,Name="Stefan",DepartmentId=1,ManagerId=2,Salary=5000,Bonus=0M},
        };
    }
}
