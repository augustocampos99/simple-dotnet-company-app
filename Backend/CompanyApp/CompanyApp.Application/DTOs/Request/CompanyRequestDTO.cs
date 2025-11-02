using CompanyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.DTOs.Request
{
    public class CompanyRequestDTO
    {
        [Required(ErrorMessage = "CNPJ is required")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1, 2, ErrorMessage = "Invalid Status (ACTIVE = 1, INACTIVE = 2)")]
        public BaseStatusEnum Status { get; set; }

    }
}
