using CompanyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Domain.Entities
{
    public class Employee
    {
        public Guid Guid { get; set; }

        public string CPF { get; set; }

        public string Name { get; set; }

        public Guid RoleId { get; set; }

        public Guid CompanyId { get; set; }

        public BaseStatusEnum Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Role Role { get; set; }

        public Company Company { get; set; }
    }
}
