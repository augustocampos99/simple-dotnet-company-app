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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly PostgreSQLContext _context;

        public CompanyRepository(PostgreSQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company?> FindByGuid(Guid guid)
        {
            return await this._context
                .Companies
                .Where(e => e.Guid == guid)
                .FirstOrDefaultAsync();
        }

        public async Task<Company?> FindByCnpjOrName(string cnpj, string name)
        {
            return await this._context
                .Companies
                .Where(e => e.CNPJ == cnpj || e.Name == name)
                .FirstOrDefaultAsync();
        }

    }
}
