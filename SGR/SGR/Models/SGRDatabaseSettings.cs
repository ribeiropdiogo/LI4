using System;
namespace SGR.Models
    {
        public class SGRDatabaseSettings : ISGRDatabaseSettings
        {
            public string FuncionarioCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface ISGRDatabaseSettings
    {
            string FuncionarioCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }

