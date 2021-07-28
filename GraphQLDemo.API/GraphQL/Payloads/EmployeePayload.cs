using GraphQLDemo.API.GraphQL.Common;
using GraphQLDemo.DataRepository.Data.Models;
using System.Collections.Generic;
namespace GraphQLDemo.API.GraphQL.Payloads
{
	public class EmployeePayload : Payload<int, Employee>
	{
		public EmployeePayload(int key, Employee data) : base(key, data) { }

		public EmployeePayload(IReadOnlyList<UserError> errors = null) : base(errors) { }

		public EmployeePayload(UserError error) : base(error) { }
	}
}
