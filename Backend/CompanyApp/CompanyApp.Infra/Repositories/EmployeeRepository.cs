using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Repositories;
using CompanyApp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Infra.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly PostgreSQLContext _context;

        public EmployeeRepository(PostgreSQLContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Employee>> FindAll(int take, int skip)
        {
            return await _context
                .Employees
                .Include(e => e.Role)
                .Include(e => e.Company)
                .Take(take)
                .Skip(skip)
                .ToListAsync();
        }

        public async Task<Employee?> FindByGuid(Guid guid)
        {
            return await this._context
                .Employees
                .Include(e => e.Role)
                .Include(e => e.Company)
                .Where(e => e.Guid == guid)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee?> FindByCpfOrName(string cpf, string name)
        {
            return await this._context
                .Employees
                .Where(e => e.CPF == cpf || e.Name == name)
                .FirstOrDefaultAsync();
        }


    }
}
