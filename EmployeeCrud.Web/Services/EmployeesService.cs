using EmployeeCrud.Data;
using EmployeeCrud.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        public DataContext _dbContext { get; }

        public EmployeeService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.ToListAsync(cancellationToken);
        }
        public async Task<Employee?> Get(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Employee?> Create(Employee emp, CancellationToken cancellationToken)
        {
            var res = await _dbContext.Employees.AddAsync(emp, cancellationToken);
            _dbContext.SaveChanges();
            return res.Entity;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var emp = await this.Get(id, cancellationToken);
            if (emp == null) return false;
            var res = _dbContext.Employees.Remove(emp);
            if(res == null) return false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, Employee updatedEmp, CancellationToken cancellationToken)
        {
            var emp = await this.Get(id, cancellationToken);
            if (emp == null) return false;
            var res = _dbContext.Employees.Update(updatedEmp);
            if (res == null) return false;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
