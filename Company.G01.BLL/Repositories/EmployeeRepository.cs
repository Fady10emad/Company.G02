using Company.G01.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDBContext context;

        public EmployeeRepository(CompanyDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await context.Employees.Include(e=>e.Department).Where(e => e.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        //private readonly CompanyDBContext _context;
        //public EmployeeRepository(CompanyDBContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}


        //public Employee? Get(int id)
        //{
        //    return _context.Employees.Find(id);
        //}

        //public int Add(Employee model)
        //{
        //    _context.Employees.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int Update(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}

    }
}
