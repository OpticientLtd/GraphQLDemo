using GraphQLDemo.DataRepository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.DataRepository
{
    public interface IUnitOfWork : IDisposable
	{
		Task<bool> CompleteAsync(CancellationToken cancellationToken);
		IDepartmentRepository Departments { get; }
		IEmployeeRepository Employees { get; }
	}
}
