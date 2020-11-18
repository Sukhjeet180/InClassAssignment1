using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : BaseAdo, IDataRepository<ApplicantResumePoco>

    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Resume]
                                               ,[Last_Updated])
                                         VALUES
                                               (@Id
                                               ,@Applicant
                                               ,@Resume
                                               ,@Last_Updated)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
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

        public IList<ApplicantResumePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Applicant]
                                          ,[Resume]
                                          ,[Last_Updated]
                                      FROM [dbo].[Applicant_Resumes]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Resume = reader.GetString(2);
                    poco.LastUpdated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<ApplicantResumePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                            WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Reume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                               SET 
                                                  [Applicant] = @Applicant
                                                  ,[Resume] = @Resume
                                                  ,[Last_Updated] = @Last_Updated
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
