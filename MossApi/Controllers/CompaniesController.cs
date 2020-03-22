using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossApi.Models;
using MossApi.Services;
using System.Collections.Generic;

namespace MossApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Gets all Companies.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Company>> Get() =>
            _companyService.Get();

        /// <summary>
        /// Gets a specific Company.
        /// </summary>
        [HttpGet("{id:length(24)}", Name = "GetCompany")]
        public ActionResult<Company> Get(string id)
        {
            var company = _companyService.Get(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        /// <summary>
        /// Adds a new Company.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Companies
        ///     {
        ///        "id": 1,
        ///        "name": "Company1",
        ///        "code" : "Code1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created company</response>
        /// <response code="400">If the company is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Company> Create(Company company)
        {
            _companyService.Create(company);

            return CreatedAtRoute("GetCompany", new { id = company.Id.ToString() }, company);
        }

        /// <summary>
        /// Edits a specific Company.
        /// </summary>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Company companyIn)
        {
            var company = _companyService.Get(id);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Update(id, companyIn);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Company.
        /// </summary>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var company = _companyService.Get(id);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Remove(company.Id);

            return NoContent();
        }
    }
}