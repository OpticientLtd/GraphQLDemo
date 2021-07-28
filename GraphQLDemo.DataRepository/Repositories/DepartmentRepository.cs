using GraphQLDemo.DataRepository.Data;
using GraphQLDemo.DataRepository.Data.Models;
using GraphQLDemo.DataRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Opticient.EFCore.Repository;
namespace GraphQLDemo.DataRepository.Repositories
{
    public class DepartmentRepository : IdRepository<Department, int, GraphQLDemoContext>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
        private GraphQLDemoContext _dbContext => base.dbContext as GraphQLDemoContext;
    }
}
