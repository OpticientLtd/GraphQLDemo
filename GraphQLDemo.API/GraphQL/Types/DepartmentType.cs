using System;
using GraphQLDemo.API.GraphQL.DataLoaders;
using GraphQLDemo.DataRepository.Data.Models;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.Types
{
	public class DepartmentType : ObjectType<Department>
	{
		protected override void Configure(IObjectTypeDescriptor<Department> descriptor)
		{
			descriptor.Field(f => f.Id).Type<NonNullType<IdType>>();
			descriptor.Field(f => f.Name).Type<NonNullType<StringType>>();
			descriptor.Field(f => f.Employees)
				.ResolveWith<Resolvers>(r => r.GetEmployeesByDepartment(default!, default!, default));
		}

		private class Resolvers
		{
			public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(Department department,
				EmployeesByDepartment_IdDataLoader dataLoader,
				CancellationToken cancellationToken)
				=> await dataLoader.LoadAsync(department.Id, cancellationToken);


		}
	}
}
