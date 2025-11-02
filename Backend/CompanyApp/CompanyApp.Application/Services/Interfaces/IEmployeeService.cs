using CompanyApp.Application.DTOs.Request;
using CompanyApp.Application.DTOs.Response;
using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDTO>> FindAll(int limit, int skip);

        Task<EmployeeResponseDTO> FindByGuid(Guid guid);

        Task<EmployeeResponseDTO> Create(EmployeeRequestDTO request);

        Task<EmployeeResponseDTO> Update(Guid guid, EmployeeRequestDTO request);

        Task Delete(Guid guid);
    }
}
