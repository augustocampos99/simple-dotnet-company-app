using CompanyApp.Application.DTOs.Request;
using CompanyApp.Application.DTOs.Response;
using CompanyApp.Application.Exceptions;
using CompanyApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Web.Controllers
{
    [Route("api/V1/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int limit = 10;
            int skip = 0;

            if (!String.IsNullOrEmpty(Request.Query["limit"]) && !String.IsNullOrEmpty(Request.Query["skip"]))
            {
                try
                {
                    limit = Int32.Parse(Request.Query["limit"]);
                    skip = Int32.Parse(Request.Query["skip"]);
                }
                catch (Exception ex)
                {
                    return BadRequest("Invalid parameter!");
                }
            }

            var result = await this._employeeService.FindAll(limit, skip);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            try 
            {
                var result = await this._employeeService.FindByGuid(guid);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Server error. Contact I.T");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._employeeService.Create(request);
                    return Ok(result);
                }
                catch (NotFoundException ex)
                {
                    return NotFound();
                }
                catch (BadRequestException ex)
                {
                    return BadRequest(new BadRequestResponseDTO { Code = 400, Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return BadRequest("Server error. Contact I.T");
                }
            }

            return BadRequest("");

        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update([FromBody] EmployeeRequestDTO request, Guid guid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._employeeService.Update(guid, request);

                    if (result == null)
                    {
                        return NotFound();
                    }

                    return Ok(result);
                }
                catch (BadRequestException ex)
                {
                    return BadRequest(new BadRequestResponseDTO { Code = 400, Message = ex.Message });
                }
                catch (NotFoundException ex)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    return BadRequest("Server error. Contact I.T");
                }
            }

            return BadRequest("");

        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try {
                await this._employeeService.Delete(guid);
                return Ok("ok");
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Server error. Contact I.T");
            }
        }

    }
}
