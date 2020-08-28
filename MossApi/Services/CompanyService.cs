using MongoDB.Driver;
using MossApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using OfficeOpenXml;

namespace MossApi.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;

        private const string AsxDownloadFilename = "ASXListedCompanies.csv";
        private const string AsxDownloadUrl = "https://www.asx.com.au/asx/research/";

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

        public void Load()
        {
            var url = string.Concat(AsxDownloadUrl, AsxDownloadFilename);

            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            var canRead = dataStream.CanRead;
            var canWrite = dataStream.CanWrite;
            var canSeek = dataStream.CanSeek;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //var byteArray = StreamToByteArray(dataStream);
            //var stream = new MemoryStream(dataStream);

            //using (var package = new ExcelPackage(stream))
            //{
            //    var worksheet = package.Workbook.Worksheets[0];
            //    var value = worksheet.Cells[0, 0].Value;
            //    value = value;
            //}
        }

        internal byte[] StreamToByteArray(Stream inputStream)
        {
            byte[] bytes = new byte[16384];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int count;
                while ((count = inputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    memoryStream.Write(bytes, 0, count);
                }
                return memoryStream.ToArray();
            }
        }
    }
}
