using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Application.DTOs.Response
{
    public class BadRequestResponseDTO
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }
}
