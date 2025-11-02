using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Domain.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> FindByGuid(Guid guid);
        Task<Company?> FindByCnpjOrName(string cnpj, string name);
    }
}
