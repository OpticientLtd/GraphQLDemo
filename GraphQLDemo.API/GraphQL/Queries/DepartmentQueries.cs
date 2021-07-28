using GraphQLDemo.API.GraphQL.DataLoaders;
using GraphQLDemo.API.GraphQL.Extensions;
using GraphQLDemo.DataRepository;
using GraphQLDemo.DataRepository.Data.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.Queries
{
	[ExtendObjectType("Query")]
	 public class DepartmentQueries
	{
		[UseApplicationDbContext]
		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<Department>> GetDepartmentsAsync([Service] IUnitOfWork unitOfWork,
			CancellationToken cancellationToken) => 
			await unitOfWork.Departments.GetAllAsync(cancellationToken: cancellationToken);

		public async Task<Department> GetDepartmentByIdAsync(int id, DepartmentByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => 
			await dataLoader.LoadAsync(id, cancellationToken);

		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<Department>> GetDepartmentsByIdsAsync(IEnumerable<int> ids, DepartmentByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => 
			await dataLoader.LoadAsync((IReadOnlyCollection<int>)ids, cancellationToken);
	}
}
