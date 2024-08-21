using API_first_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_first_project.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}
