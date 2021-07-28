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
	 public class EmployeeQueries
	{
		[UseApplicationDbContext]
		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<Employee>> GetEmployeesAsync([Service] IUnitOfWork unitOfWork,
			CancellationToken cancellationToken) => 
			await unitOfWork.Employees.GetAllAsync(cancellationToken: cancellationToken);

		public async Task<Employee> GetEmployeeByIdAsync(int id, EmployeeByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => 
			await dataLoader.LoadAsync(id, cancellationToken);

		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<Employee>> GetEmployeesByIdsAsync(IEnumerable<int> ids, EmployeeByIdDataLoader dataLoader,
			CancellationToken cancellationToken) => 
			await dataLoader.LoadAsync((IReadOnlyCollection<int>)ids, cancellationToken);
	}
}
