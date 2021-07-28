using GraphQLDemo.API.GraphQL.Common;
using GraphQLDemo.DataRepository.Data.Models;
using System.Collections.Generic;
namespace GraphQLDemo.API.GraphQL.Payloads
{
	public class DepartmentPayload : Payload<int, Department>
	{
		public DepartmentPayload(int key, Department data) : base(key, data) { }

		public DepartmentPayload(IReadOnlyList<UserError> errors = null) : base(errors) { }

		public DepartmentPayload(UserError error) : base(error) { }
	}
}
