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
    public class CompanyLocationRepository : BaseAdo, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                               ([Id]
                                               ,[Company]
                                               ,[Country_Code]
                                               ,[State_Province_Code]
                                               ,[Street_Address]
                                               ,[City_Town]
                                               ,[Zip_Postal_Code])
                                         VALUES
                                               (@Id
                                               ,@Company
                                               ,@Country_Code
                                               ,@State_Province_Code
                                               ,@Street_Address
                                               ,@City_Town
                                               ,@Zip_Postal_Code)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                              ,[Company]
                                              ,[Country_Code]
                                              ,[State_Province_Code]
                                              ,[Street_Address]
                                              ,[City_Town]
                                              ,[Zip_Postal_Code]
                                              ,[Time_Stamp]
                                          FROM [dbo].[Company_Locations]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyLocationPoco[] pocos = new CompanyLocationPoco[2000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.IsDBNull(3) ? null : reader.GetString(3);
                    poco.Street = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.City = reader.IsDBNull(5) ? null : reader.GetString(5);
                    poco.PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Locations]    
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                   
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Locations]
                                               SET 
                                                  [Company] = @Company
                                                  ,[Country_Code] = @Country_Code
                                                  ,[State_Province_Code] = @State_Province_Code
                                                  ,[Street_Address] = @Street_Address
                                                  ,[City_Town] = @City_Town
                                                  ,[Zip_Postal_Code] = @Zip_Postal_Code
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
