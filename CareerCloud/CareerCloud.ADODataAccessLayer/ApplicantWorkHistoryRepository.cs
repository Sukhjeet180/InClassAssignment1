using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : BaseAdo, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Company_Name]
                                               ,[Country_Code]
                                               ,[Location]
                                               ,[Job_Title]
                                               ,[Job_Description]
                                               ,[Start_Month]
                                               ,[Start_Year]
                                               ,[End_Month]
                                               ,[End_Year])
                                         VALUES
                                               (@Id
                                               ,@Applicant
                                               ,@Company_Name
                                               ,@Country_Code
                                               ,@Location
                                               ,@Job_Title
                                               ,@Job_Description
                                               ,@Start_Month
                                               ,@Start_Year
                                               ,@End_Month
                                               ,@End_Year)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", item.EndYear);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Company_Name]
                                      ,[Country_Code]
                                      ,[Location]
                                      ,[Job_Title]
                                      ,[Job_Description]
                                      ,[Start_Month]
                                      ,[Start_Year]
                                      ,[End_Month]
                                      ,[End_Year]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Work_History]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);
                    poco.TimeStamp = (byte[])reader[11];
                    pocos[counter] = poco;
                    counter++;


                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History]
                                             WHERE [Id] = @Id ";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                           SET
                                              [Applicant] = @Applicant
                                              ,[Company_Name] = @Company_Name
                                              ,[Country_Code] = @Country_Code
                                              ,[Location] = @Location
                                              ,[Job_Title] = @Job_Title
                                              ,[Job_Description] = @Job_Description
                                              ,[Start_Month] = @Start_Month
                                              ,[Start_Year] = @Start_Year
                                              ,[End_Month] = @End_Month
                                              ,[End_Year] = @End_Year
                                         WHERE  [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", item.EndYear);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }
    }
}
