using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MossApi.Models
{
    public class MossDatabaseSettings : IMossDatabaseSettings
    {
        public string CompaniesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMossDatabaseSettings
    {
        string CompaniesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
