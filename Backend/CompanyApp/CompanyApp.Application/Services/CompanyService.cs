using CompanyApp.Application.DTOs.Request;
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
    public class CompanyService : ICompanyService
    {

        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> FindAll(int limit, int skip)
        {
            return await this._companyRepository.FindAll(limit, skip);
        }

        public async Task<Company> FindByGuid(Guid guid)
        {
            var company = await this._companyRepository.FindByGuid(guid);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            return company;
        }

        public async Task<Company> Create(CompanyRequestDTO request)
        {
            request.CNPJ = request.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            var existCompany = await this.GetCompanyByCnpjOrName(request.CNPJ, request.Name);

            if (existCompany != null)
            {
                throw new BadRequestException("Company CNPJ or Name already exists");
            }

            Company company = new Company { 
                CNPJ = request.CNPJ,
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await this._companyRepository.Create(company);
        }

        public async Task<Company> Update(Guid guid, CompanyRequestDTO request)
        {
            var company = await this._companyRepository.FindByGuid(guid);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            company.Name = request.Name;
            company.Description = request.Description;
            company.Status = request.Status;
            company.CreatedAt = company.CreatedAt.ToUniversalTime();
            company.UpdatedAt = DateTime.UtcNow;
            return await this._companyRepository.Update(company);
        }

        public async Task Delete(Guid guid)
        {
            var company = await this._companyRepository.FindByGuid(guid);

            if(company == null)
            {
                throw new NotFoundException("Company not found");
            }

            await this._companyRepository.Delete(company);

        }

        private async Task<Company> GetCompanyByCnpjOrName(string cnpj, string name)
        {
            var company = await this._companyRepository.FindByCnpjOrName(cnpj, name);
            return company;

        }
    }
}
