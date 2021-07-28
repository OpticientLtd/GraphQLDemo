using GraphQLDemo.DataRepository.Data;
using GraphQLDemo.DataRepository.Data.Models;
using GraphQLDemo.DataRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Opticient.EFCore.Repository;
namespace GraphQLDemo.DataRepository.Repositories
{
    public class EmployeeRepository : IdRepository<Employee, int, GraphQLDemoContext>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext)
        {
        }
        private GraphQLDemoContext _dbContext => base.dbContext as GraphQLDemoContext;
    }
}
