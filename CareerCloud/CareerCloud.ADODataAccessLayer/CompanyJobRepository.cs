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
    public class CompanyJobRepository : BaseAdo, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                               ([Id]
                                               ,[Company]
                                               ,[Profile_Created]
                                               ,[Is_Inactive]
                                               ,[Is_Company_Hidden])
                                         VALUES
                                               (@Id
                                               ,@Company
                                               ,@Profile_Created
                                               ,@Is_Inactive
                                               ,@Is_Company_Hidden)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Company]
                                          ,[Profile_Created]
                                          ,[Is_Inactive]
                                          ,[Is_Company_Hidden]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Company_Jobs]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobPoco[] pocos = new CompanyJobPoco[2000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.ProfileCreated = (DateTime)reader[2];
                    poco.IsInactive = reader.GetBoolean(3);
                    poco.IsCompanyHidden = reader.GetBoolean(4);
                    poco.TimeStamp = (byte[])reader[5];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }

        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                                                WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }

        
    }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                               SET 
                                                  [Company] = @Company
                                                  ,[Profile_Created] = @Profile_Created
                                                  ,[Is_Inactive] = @Is_Inactive
                                                  ,[Is_Company_Hidden] = @Is_Company_Hidden
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }

        
    }
    }
}
