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
    public class CompanyJobDescriptionRepository : BaseAdo, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                           ([Id]
                                           ,[Job]
                                           ,[Job_Name]
                                           ,[Job_Descriptions])
                                     VALUES
                                           (@Id
                                           ,@Job
                                           ,@Job_Name
                                           ,@Job_Descriptions)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Job_Name", item.JobName);
                    command.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                            ,[Job]
                                            ,[Job_Name]
                                            ,[Job_Descriptions]
                                            ,[Time_Stamp]
                                             FROM [dbo].[Company_Jobs_Descriptions]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[2000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.JobName = reader.GetString(2);
                    poco.JobDescriptions = reader.GetString(3);              
                    poco.TimeStamp = (byte[])reader[4];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }

        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                         WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                           SET 
                                              [Job] = @Job
                                              ,[Job_Name] = @Job_Name
                                              ,[Job_Descriptions] = @Job_Descriptions
                                         WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Job_Name", item.JobName);
                    command.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
