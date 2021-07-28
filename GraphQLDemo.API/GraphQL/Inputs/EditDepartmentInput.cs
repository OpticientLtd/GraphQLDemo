using System;
using HotChocolate;
#nullable enable
namespace GraphQLDemo.API.GraphQL.Inputs
{
	public record EditDepartmentInput(
		int Id,
		Optional<string?> Name
	);
}
