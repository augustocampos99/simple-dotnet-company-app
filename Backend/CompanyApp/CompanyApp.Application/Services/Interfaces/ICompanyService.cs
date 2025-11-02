using CompanyApp.Application.DTOs.Request;
using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<Company>> FindAll(int limit, int skip);

        Task<Company> FindByGuid(Guid guid);

        Task<Company> Create(CompanyRequestDTO request);

        Task<Company> Update(Guid guid, CompanyRequestDTO request);

        Task Delete(Guid guid);
    }
}
