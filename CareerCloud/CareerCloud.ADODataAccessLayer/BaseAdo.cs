using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
  public abstract class BaseAdo
    {
        public string _connectionString;
        public BaseAdo()
        {
           
            _connectionString= @"Data Source=SUKH-HP\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(),"appsettings.json");
            config.AddJsonFile(path,false);
            var root = config.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

        }
    }
}
