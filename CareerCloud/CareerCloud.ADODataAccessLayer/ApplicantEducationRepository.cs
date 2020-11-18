using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseAdo, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                        ([Id]
                                       ,[Applicant]
                                       ,[Major]
                                       ,[Certificate_Diploma]
                                       ,[Start_Date]
                                       ,[Completion_Date]
                                       ,[Completion_Percent])
                                 VALUES
                                       (@Id,@Applicant
                                       ,@Major
                                       ,@Certificate_Diploma
                                       ,@Start_Date
                                       ,@Completion_Date
                                       ,@Completion_Percent)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Major]
                                      ,[Certificate_Diploma]
                                      ,[Start_Date]
                                      ,[Completion_Date]
                                      ,[Completion_Percent]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Educations]";

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                    poco.CompletionDate = (DateTime?)reader.GetSqlValue(5);
                    poco.CompletionPercent = (byte?)reader[6];
                    poco.TimeStamp = (byte[])reader[7];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM[dbo].[Applicant_Educations]
                                         WHERE  [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", item.Id);


                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void Update(params ApplicantEducationPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            { 
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"UPDATE[dbo].[Applicant_Educations]
                                       SET[Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Major] = @Major
                                          ,[Certificate_Diploma] = @Certificate_Diploma
                                          ,[Start_Date] = @Start_Date
                                          ,[Completion_Date] = @Completion_Date
                                          ,[Completion_Percent] = @Completion_Percent
                                      WHERE [Id]=@Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }


            }
        }
    }
}
