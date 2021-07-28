using GraphQLDemo.DataRepository.Data.Models;
using Opticient.EFCore.Repository;
namespace GraphQLDemo.DataRepository.Interfaces
{
    public interface IEmployeeRepository : IIdRepository<Employee, int>
    {
    }
}
