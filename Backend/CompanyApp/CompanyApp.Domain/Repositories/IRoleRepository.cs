using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> FindByGuid(Guid guid);
    }
}
