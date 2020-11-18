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
    public class CompanyDescriptionRepository : BaseAdo, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                               ([Id]
                                               ,[Company]
                                               ,[LanguageID]
                                               ,[Company_Name]
                                               ,[Company_Description])
                                         VALUES
                                               (@Id
                                               ,@Company
                                               ,@LanguageID
                                               ,@Company_Name
                                               ,@Company_Description)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Company]
                                          ,[LanguageID]
                                          ,[Company_Name]
                                          ,[Company_Description]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Company_Descriptions]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.LanguageId = reader.GetString(2);
                    poco.CompanyName = reader.GetString(3);
                    poco.CompanyDescription = reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }

        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                            WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                               SET [Id] = @Id
                                                  ,[Company] = @Company
                                                  ,[LanguageID] = @LanguageID
                                                  ,[Company_Name] = @Company_Name
                                                  ,[Company_Description] = @Company_Description
                                             WHERE [Id] = @Id"; 
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
