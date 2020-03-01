using MongoDB.Driver;
using MossApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MossApi.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;

        public CompanyService(IMossDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _companies = database.GetCollection<Company>(settings.CompaniesCollectionName);
        }

        public List<Company> Get() =>
            _companies.Find(company => true).ToList();

        public Company Get(string id) =>
            _companies.Find<Company>(company => company.Id == id).FirstOrDefault();

        public Company Create(Company company)
        {
            _companies.InsertOne(company);
            return company;
        }

        public void Update(string id, Company companyIn) =>
            _companies.ReplaceOne(company => company.Id == id, companyIn);

        public void Remove(Company companyIn) =>
            _companies.DeleteOne(company => company.Id == companyIn.Id);

        public void Remove(string id) =>
            _companies.DeleteOne(company => company.Id == id);
    }
}
