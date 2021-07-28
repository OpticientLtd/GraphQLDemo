using System;
using GraphQLDemo.DataRepository;
using GraphQLDemo.DataRepository.Data.Models;
using GreenDonut;
using HotChocolate;
using HotChocolate.DataLoader;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.DataLoaders
{
	public class EmployeesByEmployee_IdDataLoader : GroupedDataLoader<int, Employee>
	{
		private readonly IUnitOfWork _unitOfWork;
		public EmployeesByEmployee_IdDataLoader(IBatchScheduler batchScheduler,
			[Service] IUnitOfWork unitOfWork) : base(batchScheduler)
		{
			_unitOfWork = unitOfWork;
		}
		protected async override Task<ILookup<int, Employee>> LoadGroupedBatchAsync(IReadOnlyList<int> keys,
			CancellationToken cancellationToken)
		{
			return (await _unitOfWork.Employees
					.GetAllAsync(predicate: x => keys.Contains(x.ManagerId.Value), cancellationToken: cancellationToken))
				.ToLookup(x => x.ManagerId.Value);
		}
	}
}
