using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository : BaseAdo, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                               ([Code]
                                               ,[Name])
                                         VALUES
                                               (@Code
                                               ,@Name)";
                    command.Parameters.AddWithValue("@Code", item.Code);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Code]
                                              ,[Name]
                                          FROM [dbo].[System_Country_Codes]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = reader.GetString(0);
                    poco.Name = reader.GetString(1);                    
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                             WHERE [Code] = @Code";
                    command.Parameters.AddWithValue("@Code", item.Code);                   
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                               SET 
                                                  [Name] = @Name
                                             WHERE [Code] = @Code";
                    command.Parameters.AddWithValue("@Code", item.Code);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
