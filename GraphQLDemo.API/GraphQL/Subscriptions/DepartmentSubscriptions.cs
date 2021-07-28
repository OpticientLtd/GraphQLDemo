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
	 public class DepartmentSubscriptions
	{
		[Subscribe]
		[Topic]
		public Task<Department> OnDepartmentAddedAsync(
			[EventMessage] int id,
			DepartmentByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

		[Subscribe]
		[Topic]
		public Task<Department> OnDepartmentEditedAsync(
			[EventMessage] int id,
			DepartmentByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

		[Subscribe]
		[Topic]
		public Task<DepartmentPayload> OnDepartmentDeletedAsync([EventMessage] DepartmentPayload payload)
			=> Task.FromResult<DepartmentPayload>(payload);
	}
}
