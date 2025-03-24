using Company.G01.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly CompanyDBContext _context;

        public IDepartmentRepository DepartmentRepository { get; }

       public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDBContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
