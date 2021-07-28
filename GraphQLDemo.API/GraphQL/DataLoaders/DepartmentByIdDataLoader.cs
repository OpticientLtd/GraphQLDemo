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
	public class DepartmentByIdDataLoader : BatchDataLoader<int, Department>
	{
		private readonly IUnitOfWork _unitOfWork;
		public DepartmentByIdDataLoader(IBatchScheduler batchScheduler,
			[Service] IUnitOfWork unitOfWork) : base(batchScheduler)
		{
			_unitOfWork = unitOfWork;
		}
		protected async override Task<IReadOnlyDictionary<int, Department>> LoadBatchAsync(IReadOnlyList<int> keys,
			CancellationToken cancellationToken)
		{
			return (await _unitOfWork.Departments
					.GetAllAsync(predicate: x => keys.Contains(x.Id), cancellationToken: cancellationToken))
				.ToDictionary(x => x.Id);
		}
	}
}
