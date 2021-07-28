using System;
using HotChocolate;
#nullable enable
namespace GraphQLDemo.API.GraphQL.Inputs
{
	public record EditEmployeeInput(
		int Id,
		Optional<string?> Name,
		Optional<int?> DepartmentId,
		Optional<int?> ManagerId,
		Optional<int?> Salary,
		Optional<decimal?> Bonus
	);
}
