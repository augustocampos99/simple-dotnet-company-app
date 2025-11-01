using CompanyApp.Application.DTOs.Request;
using CompanyApp.Application.Exceptions;
using CompanyApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Web.Controllers
{
    [Route("api/V1/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;            
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

            var result = await this._roleService.FindAll(limit, skip);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            try {
                var result = await this._roleService.FindByGuid(guid);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await this._roleService.Create(request);
                return Ok(result);
            }

            return BadRequest("");

        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update([FromBody] RoleRequestDTO request, Guid guid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._roleService.Update(guid, request);

                    if (result == null)
                    {
                        return NotFound();
                    }

                    return Ok(result);
                }
                catch (NotFoundException ex)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("");

        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try {
                await this._roleService.Delete(guid);
                return Ok("ok");
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
