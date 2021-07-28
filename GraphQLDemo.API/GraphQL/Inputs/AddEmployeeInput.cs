using System;
using HotChocolate;
#nullable enable
namespace GraphQLDemo.API.GraphQL.Inputs
{
	public record AddEmployeeInput(
		string Name,
		int DepartmentId,
		Optional<int?> ManagerId,
		int Salary,
		Optional<decimal?> Bonus
	);
}
