
using EmployeeCrud.Domain;
using EmployeeCrud.Web.Models.DTO;
using EmployeeCrud.Web.Services;

namespace EmployeeCrud.Test.MockData
{
    public class EmployeeServiceMockData : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeServiceMockData()
        {
            _employees = new List<Employee>{
                new Employee{
                    Id = 1,
                    FirstName = "Malouda",
                    LastName = "Tombotou",
                    Email = "malouda@gmail.com"
                },
                new Employee{
                    Id = 2,
                    FirstName = "Stewart",
                    LastName = "coffee",
                    Email = "stew@gmail.com"
                },
                new Employee{
                    Id = 3,
                    FirstName = "david",
                    LastName = "fooster",
                    Email = "david@gmail.com"
                }
            };
        }

        public async Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _employees);
        }

        public async Task<Employee?> Create(EmployeeDTO dto, CancellationToken cancellationToken)
        {
            var newEmp = new Employee()
            {
                Id = _employees.Last().Id + 1,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.FirstName,
            };
            await Task.Run(() => _employees.Add(newEmp));
            return newEmp;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var existing = _employees.FirstOrDefault(x => x.Id == id);
            if (existing == null) return false;
            await Task.Run(() => _employees.Remove(existing));
            return true;
        }

        public async Task<Employee?> Get(int id, CancellationToken cancellationToken)
        {
            var existing = await Task.Run(() => _employees.FirstOrDefault(x => x.Id == id));
            return existing;
        }

        public async Task<bool> Update(int id, Employee dto, CancellationToken cancellationToken)
        {
            var i = await Task.Run(() => _employees.FindIndex(x => x.Id == id));
            if (i == -1) return false;
            var newOne = new Employee()
            {
                Id = id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.FirstName,
            };
            _employees[i] = newOne;
            return true;
        }
    }
}
