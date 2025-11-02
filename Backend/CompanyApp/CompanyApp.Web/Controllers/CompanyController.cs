using CompanyApp.Application.DTOs.Request;
using CompanyApp.Application.DTOs.Response;
using CompanyApp.Application.Exceptions;
using CompanyApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Web.Controllers
{
    [Route("api/V1/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;            
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

            var result = await this._companyService.FindAll(limit, skip);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            try 
            {
                var result = await this._companyService.FindByGuid(guid);
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
        public async Task<IActionResult> Create([FromBody] CompanyRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._companyService.Create(request);
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
                    return BadRequest("Server error. Contact I.T" + ex.Message);
                }
            }

            return BadRequest("");

        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update([FromBody] CompanyRequestDTO request, Guid guid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._companyService.Update(guid, request);

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
                await this._companyService.Delete(guid);
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
