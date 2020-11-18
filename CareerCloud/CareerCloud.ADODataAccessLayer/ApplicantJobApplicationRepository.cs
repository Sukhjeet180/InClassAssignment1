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
   public class ApplicantJobApplicationRepository : BaseAdo, IDataRepository<ApplicantJobApplicationPoco>

    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Job]
                                           ,[Application_Date])
                                     VALUES
                                           (@Id
                                           ,@Applicant
                                           ,@Job
                                           ,@Application_Date)";
                      
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                    
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

        public IList<ApplicantJobApplicationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Job]
                                      ,[Application_Date]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Job_Applications]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];
                    pocos[counter] = poco;
                    counter++;



                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                                WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", item.Id);


                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }

                
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                           SET 
                                              [Applicant] = @Applicant
                                              ,[Job] = @Job
                                              ,[Application_Date] = @Application_Date
                                         WHERE [Id]=@Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();

                }

            }
        }
    }

    }
