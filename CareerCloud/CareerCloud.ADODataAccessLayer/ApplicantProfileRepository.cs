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
    public class ApplicantProfileRepository : BaseAdo, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                           ([Id]
                                           ,[Login]
                                           ,[Current_Salary]
                                           ,[Current_Rate]
                                           ,[Currency]
                                           ,[Country_Code]
                                           ,[State_Province_Code]
                                           ,[Street_Address]
                                           ,[City_Town]
                                           ,[Zip_Postal_Code])
                                     VALUES
                                           (@Id
                                           ,@Login
                                           ,@Current_Salary
                                           ,@Current_Rate
                                           ,@Currency
                                           ,@Country_Code
                                           ,@State_Province_Code
                                           ,@Street_Address
                                           ,@City_Town
                                           ,@Zip_Postal_Code)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", item.Currency);
                    command.Parameters.AddWithValue("@Country_Code", item.Country);
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Login]
                                          ,[Current_Salary]
                                          ,[Current_Rate]
                                          ,[Currency]
                                          ,[Country_Code]
                                          ,[State_Province_Code]
                                          ,[Street_Address]
                                          ,[City_Town]
                                          ,[Zip_Postal_Code]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Applicant_Profiles]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = (decimal?)reader[2];
                    poco.CurrentRate = (decimal?)reader[3];
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street= reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode= reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                            WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);



                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                           SET 
                                              [Login] = @Login
                                              ,[Current_Salary] = @Current_Salary
                                              ,[Current_Rate] = @Current_Rate
                                              ,[Currency] = @Currency
                                              ,[Country_Code] = @Country_Code
                                              ,[State_Province_Code] = @State_Province_Code
                                              ,[Street_Address] = @Street_Address
                                              ,[City_Town] = @City_Town
                                              ,[Zip_Postal_Code] = @Zip_Postal_Code
                                         WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", item.Currency);
                    command.Parameters.AddWithValue("@Country_Code", item.Country);
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
