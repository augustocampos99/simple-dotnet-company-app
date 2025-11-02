using CompanyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Domain.Entities
{
    public class Company
    {
        public Guid Guid { get; set; }

        public string CNPJ { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public BaseStatusEnum Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
