using AutoMapper;
using EmployeeCrud.Data;
using EmployeeCrud.Domain;
using EmployeeCrud.Web.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        public DataContext _dbContext { get; }
        public IMapper _mapper { get; }

        public EmployeeService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<Employee?> Get(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Employee?> Create(EmployeeDTO empDto, CancellationToken cancellationToken)
        {
            var emp = _mapper.Map<Employee>(empDto);
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
