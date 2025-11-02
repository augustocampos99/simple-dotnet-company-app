using CompanyApp.Application.DTOs.Request;
using CompanyApp.Application.DTOs.Response;
using CompanyApp.Application.Exceptions;
using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository, 
            IRoleRepository roleRepository, 
            ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _companyRepository = companyRepository;
        }

        public async Task<List<EmployeeResponseDTO>> FindAll(int limit, int skip)
        {
            var employeeList = await this._employeeRepository.FindAll(limit, skip);
            var result = employeeList.Select(e => new EmployeeResponseDTO 
            {
                Guid = e.Guid,
                CPF = e.CPF,
                Name = e.Name,
                Status = e.Status,
                RoleName = e.Role.Name,
                CompanyName =  e.Company.Name
            }).ToList();

            return result;
        }

        public async Task<EmployeeResponseDTO> FindByGuid(Guid guid)
        {
            var employee = await this._employeeRepository.FindByGuid(guid);

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            return new EmployeeResponseDTO
            {
                Guid = employee.Guid,
                CPF = employee.CPF,
                Name = employee.Name,
                Status = employee.Status,
                RoleName = employee.Role.Name,
                CompanyName = employee.Company.Name
            };
        }

        public async Task<EmployeeResponseDTO> Create(EmployeeRequestDTO request)
        {
            request.CPF = request.CPF.Replace(".", "").Replace("-", "");
            var existEmployee = await this.GetEmployeeByCpfOrName(request.CPF, request.Name);
            if (existEmployee != null)
            {
                throw new BadRequestException("Employee CPF or Name already exists");
            }

            var existRole = await this._roleRepository.FindByGuid(request.RoleId);
            if (existRole == null)
            {
                throw new BadRequestException("Role not found");
            }

            var existCompany = await this._companyRepository.FindByGuid(request.CompanyId);
            if (existCompany == null)
            {
                throw new BadRequestException("Company not found");
            }

            Employee employee = new Employee { 
                CPF = request.CPF,
                Name = request.Name,
                RoleId = request.RoleId,
                CompanyId = request.CompanyId,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var employeeResult = await this._employeeRepository.Create(employee);

            return new EmployeeResponseDTO
            {
                Guid = employeeResult.Guid,
                CPF = employeeResult.CPF,
                Name = employeeResult.Name,
                Status = employeeResult.Status,
                RoleName = employeeResult.Role.Name,
                CompanyName = employeeResult.Company.Name
            };
        }

        public async Task<EmployeeResponseDTO> Update(Guid guid, EmployeeRequestDTO request)
        {
            var employee = await this._employeeRepository.FindByGuid(guid);

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            var existRole = await this._roleRepository.FindByGuid(request.RoleId);
            if (existRole == null)
            {
                throw new BadRequestException("Role not found");
            }

            var existCompany = await this._companyRepository.FindByGuid(request.CompanyId);
            if (existCompany == null)
            {
                throw new BadRequestException("Company not found");
            }

            employee.Name = request.Name;
            employee.RoleId = request.RoleId;
            employee.CompanyId = request.CompanyId;
            employee.Status = request.Status;
            employee.CreatedAt = employee.CreatedAt.ToUniversalTime();
            employee.UpdatedAt = DateTime.UtcNow;
            var employeeResult = await this._employeeRepository.Update(employee);

            return new EmployeeResponseDTO
            {
                Guid = employeeResult.Guid,
                CPF = employeeResult.CPF,
                Name = employeeResult.Name,
                Status = employeeResult.Status,
                RoleName = employeeResult.Role.Name,
                CompanyName = employeeResult.Company.Name
            };
        }

        public async Task Delete(Guid guid)
        {
            var employee = await this._employeeRepository.FindByGuid(guid);

            if(employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            await this._employeeRepository.Delete(employee);

        }

        private async Task<Employee> GetEmployeeByCpfOrName(string cpf, string name)
        {
            var employee = await this._employeeRepository.FindByCpfOrName(cpf, name);
            return employee;

        }

    }
}
