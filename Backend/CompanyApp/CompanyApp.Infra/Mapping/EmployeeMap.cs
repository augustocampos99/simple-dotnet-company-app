using CompanyApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Infra.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");
            builder.HasKey(e => e.Guid);

            builder.Property(e => e.Guid).HasColumnName("guid");
            builder.Property(e => e.CPF).HasColumnName("cpf");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.RoleId).HasColumnName("role_id");
            builder.Property(e => e.CompanyId).HasColumnName("company_id");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            builder.Ignore(e => e.Role);
            builder.Ignore(e => e.Company);

            builder.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId);
            builder.HasOne(e => e.Company).WithMany().HasForeignKey(e => e.CompanyId);
        }
    }
}
