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
	public class EmployeesByDepartment_IdDataLoader : GroupedDataLoader<int, Employee>
	{
		private readonly IUnitOfWork _unitOfWork;
		public EmployeesByDepartment_IdDataLoader(IBatchScheduler batchScheduler,
			[Service] IUnitOfWork unitOfWork) : base(batchScheduler)
		{
			_unitOfWork = unitOfWork;
		}
		protected async override Task<ILookup<int, Employee>> LoadGroupedBatchAsync(IReadOnlyList<int> keys,
			CancellationToken cancellationToken)
		{
			return (await _unitOfWork.Employees
					.GetAllAsync(predicate: x => keys.Contains(x.DepartmentId), cancellationToken: cancellationToken))
				.ToLookup(x => x.DepartmentId);
		}
	}
}
