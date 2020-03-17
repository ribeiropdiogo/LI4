using System;
namespace SGR.Models
    {
        public class SGRDatabaseSettings : ISGRDatabaseSettings
        {
            public string FuncionáriosCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface ISGRDatabaseSettings
    {
            string FuncionáriosCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }

