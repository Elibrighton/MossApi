﻿using Microsoft.AspNetCore.Mvc;
using MossApi.Models;
using MossApi.Services;
using System.Collections.Generic;

namespace MossApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult<List<Company>> Get() =>
            _companyService.Get();

        //[HttpGet("{id:length(24)}", Name = "GetCompany")]
        //public ActionResult<Company> Get(string id)
        //{
        //    var company = _companyService.Get(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return company;
        //}

        //[HttpPost]
        //public ActionResult<Company> Create(Company company)
        //{
        //    _companyService.Create(company);

        //    return CreatedAtRoute("GetCompany", new { id = company.Id.ToString() }, company);
        //}

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Company companyIn)
        //{
        //    var company = _companyService.Get(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _companyService.Update(id, companyIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var company = _companyService.Get(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _companyService.Remove(company.Id);

        //    return NoContent();
        //}
    }
}