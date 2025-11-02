using CompanyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.DTOs.Request
{
    public class EmployeeRequestDTO
    {
        [Required(ErrorMessage = "CPF is required")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RoleId is required")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "CompanyId is required")]
        public Guid CompanyId { get; set; }

        [Range(1, 2, ErrorMessage = "Invalid Status (ACTIVE = 1, INACTIVE = 2)")]
        public BaseStatusEnum Status { get; set; }
    }
}
