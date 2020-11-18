
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //string s = ConfigurationManager.ConnectionStrings["DataConnection"].ToString();
            //string s = ConfigurationManager.AppSettings["ConnectionStrings.DataConnection"];

            //  string s =ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

            /* IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()

             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")).AddJsonFile("appsettings.json",
                 optional: true,
                 reloadOnChange: true);*/

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName).AddJsonFile("appsettings.json").Build();


            

            string s = ConfigurationManager.AppSettings["ConnectionStrings"];
            
         
            Console.WriteLine("Hello World!");
            Console.WriteLine(s);
        }
    }

   
  
}
