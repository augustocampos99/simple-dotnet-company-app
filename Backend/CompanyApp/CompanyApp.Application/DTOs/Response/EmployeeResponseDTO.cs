using CompanyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.DTOs.Response
{
    public class EmployeeResponseDTO
    {
        public Guid Guid { get; set; }

        public string CPF { get; set; }

        public string Name { get; set; }

        public BaseStatusEnum Status { get; set; }

        public string RoleName { get; set; }

        public string CompanyName { get; set; }

    }
}
