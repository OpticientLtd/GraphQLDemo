using GraphQLDemo.API.GraphQL.DataLoaders;
using GraphQLDemo.DataRepository.Data.Models;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.Types
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>();
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.DepartmentId).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Department).Type<NonNullType<DepartmentType>>()
                .ResolveWith<Resolvers>(r => r.GetDepartmentByEmployee(default!, default!, default));
            descriptor.Field(f => f.ManagerId).Type<IntType>();
            descriptor.Field(f => f.Manager).Type<EmployeeType>()
                .ResolveWith<Resolvers>(r => r.GetEmployeeByEmployee(default!, default!, default));
            descriptor.Field(f => f.Salary).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Bonus).Type<DecimalType>();
            descriptor.Field(f => f.ManagedEmployees)
                .ResolveWith<Resolvers>(r => r.GetManagedEmployeesByEmployee(default!, default!, default));
        }

        private class Resolvers
        {
            public async Task<Department> GetDepartmentByEmployee(Employee employee,
                DepartmentByIdDataLoader dataLoader,
                CancellationToken cancellationToken)
                => await dataLoader.LoadAsync(employee.DepartmentId, cancellationToken);

            public async Task<Employee> GetEmployeeByEmployee(Employee employee,
                EmployeeByIdDataLoader dataLoader,
                CancellationToken cancellationToken)
            {
                if (employee.ManagerId.HasValue)
                    return await dataLoader.LoadAsync(employee.ManagerId.Value, cancellationToken);
                else
                    return await Task.FromResult<Employee>(null);
            }

            public async Task<IEnumerable<Employee>> GetManagedEmployeesByEmployee(Employee employee,
                EmployeesByEmployee_IdDataLoader dataLoader,
                CancellationToken cancellationToken)
                => await dataLoader.LoadAsync(employee.Id, cancellationToken);


        }
    }
}
