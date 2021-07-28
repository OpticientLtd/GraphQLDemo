using GraphQLDemo.DataRepository.Data;
using GraphQLDemo.DataRepository.Interfaces;
using GraphQLDemo.DataRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.DataRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GraphQLDemoContext _dbContext;
        public UnitOfWork(IDbContextFactory<GraphQLDemoContext> _contextFactory)
        {
            if (_contextFactory == null)
                throw new System.ArgumentNullException(nameof(_contextFactory));
            _dbContext = _contextFactory.CreateDbContext();
            Departments = new DepartmentRepository(_dbContext);
            Employees = new EmployeeRepository(_dbContext);
        }
        public IDepartmentRepository Departments { get; private set; }
        public IEmployeeRepository Employees { get; private set; }

        public async Task<bool> CompleteAsync(CancellationToken cancellationToken)
        {
            return (await _dbContext.SaveChangesAsync(cancellationToken)) > 0;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
