using GraphQLDemo.DataRepository.Data.Models;
using Opticient.EFCore.Repository;
namespace GraphQLDemo.DataRepository.Interfaces
{
    public interface IDepartmentRepository : IIdRepository<Department, int>
    {
    }
}
