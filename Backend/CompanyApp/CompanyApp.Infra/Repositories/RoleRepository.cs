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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly PostgreSQLContext _context;

        public RoleRepository(PostgreSQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role?> FindByGuid(Guid guid)
        {
            return await this._context
                .Roles
                .Where(e => e.Guid == guid)
                .FirstOrDefaultAsync();
        }


    }
}
