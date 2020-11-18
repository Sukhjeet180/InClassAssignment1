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
    public class CompanyProfileRepository : BaseAdo, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    foreach (CompanyProfilePoco item in items)
                    {
                        command.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                                   ([Id]
                                                   ,[Registration_Date]
                                                   ,[Company_Website]
                                                   ,[Contact_Phone]
                                                   ,[Contact_Name]
                                                   ,[Company_Logo])
                                             VALUES
                                                   (@Id
                                                   ,@Registration_Date
                                                   ,@Company_Website
                                                   ,@Contact_Phone
                                                   ,@Contact_Name
                                                   ,@Company_Logo)";
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Registration_Date]
                                          ,[Company_Website]
                                          ,[Contact_Phone]
                                          ,[Contact_Name]
                                          ,[Company_Logo]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Company_Profiles]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[2000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = (DateTime)reader[1];
                    poco.CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.CompanyLogo= reader.IsDBNull(5) ? null : (byte[])reader[5];
                    //poco.CompanyLogo = (byte[])reader[5];               
                    poco.TimeStamp = (byte[])reader[6];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                            WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                               SET 
                                                  [Registration_Date] = @Registration_Date
                                                  ,[Company_Website] = @Company_Website
                                                  ,[Contact_Phone] = @Contact_Phone
                                                  ,[Contact_Name] = @Contact_Name
                                                  ,[Company_Logo] = @Company_Logo
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
