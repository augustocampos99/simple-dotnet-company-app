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
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> FindAll(int limit, int skip)
        {
            return await this._roleRepository.FindAll(limit, skip);
        }

        public async Task<Role> FindByGuid(Guid guid)
        {
            var role = await this._roleRepository.FindByGuid(guid);

            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            return role;
        }

        public async Task<Role> Create(RoleRequestDTO request)
        {
            var existRole = await this.GetRoleByName(request.Name);

            if(existRole != null)
            {
                throw new BadRequestException("Role name already exists");
            }

            Role role = new Role { 
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await this._roleRepository.Create(role);
        }

        public async Task<Role> Update(Guid guid, RoleRequestDTO request)
        {
            var role = await this._roleRepository.FindByGuid(guid);

            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            var existRole = await this.GetRoleByName(request.Name);

            if (existRole != null)
            {
                throw new BadRequestException("Role name already exists");
            }

            role.Name = request.Name;
            role.CreatedAt = role.CreatedAt.ToUniversalTime();
            role.UpdatedAt = DateTime.UtcNow;
            return await this._roleRepository.Update(role);
        }

        public async Task Delete(Guid guid)
        {
            var role = await this._roleRepository.FindByGuid(guid);

            if(role == null)
            {
                throw new NotFoundException("Role not found");
            }

            await this._roleRepository.Delete(role);

        }

        private async Task<Role> GetRoleByName(string name)
        {
            var role = await this._roleRepository.FindByName(name);
            return role;

        }
    }
}
