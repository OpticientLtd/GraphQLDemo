using GraphQLDemo.API.GraphQL.DataLoaders;
using GraphQLDemo.API.GraphQL.Payloads;
using GraphQLDemo.DataRepository.Data.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.Subscriptions
{
	[ExtendObjectType("Subscription")]
	 public class EmployeeSubscriptions
	{
		[Subscribe]
		[Topic]
		public Task<Employee> OnEmployeeAddedAsync(
			[EventMessage] int id,
			EmployeeByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

		[Subscribe]
		[Topic]
		public Task<Employee> OnEmployeeEditedAsync(
			[EventMessage] int id,
			EmployeeByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

		[Subscribe]
		[Topic]
		public Task<EmployeePayload> OnEmployeeDeletedAsync([EventMessage] EmployeePayload payload)
			=> Task.FromResult<EmployeePayload>(payload);
	}
}
