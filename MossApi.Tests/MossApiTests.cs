using MossApi.Models;
using MossApi.Services;
using NUnit.Framework;

namespace MossApi.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var settings = new MossDatabaseSettings();
            var companyService = new CompanyService(settings);

            // Act
            companyService.Load();

            // Assert
            Assert.Pass();
        }
    }
}