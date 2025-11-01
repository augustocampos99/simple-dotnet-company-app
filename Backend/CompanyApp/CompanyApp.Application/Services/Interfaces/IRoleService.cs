using CompanyApp.Application.DTOs.Request;
using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> FindAll(int limit, int skip);

        Task<Role> FindByGuid(Guid guid);

        Task<Role> Create(RoleRequestDTO request);

        Task<Role> Update(Guid guid, RoleRequestDTO request);

        Task Delete(Guid guid);
    }
}
