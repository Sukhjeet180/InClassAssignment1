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
    public class SecurityLoginsLogRepository : BaseAdo, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                               ([Id]
                                               ,[Login]
                                               ,[Source_IP]
                                               ,[Logon_Date]
                                               ,[Is_Succesful])
                                         VALUES
                                               (@Id
                                               ,@Login
                                               ,@Source_IP
                                               ,@Logon_Date
                                               ,@Is_Succesful)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Login]
                                          ,[Source_IP]
                                          ,[Logon_Date]
                                          ,[Is_Succesful]
                                      FROM [dbo].[Security_Logins_Log]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[2000];
                int counter = 0;

                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.SourceIP = reader.GetString(2);
                    poco.LogonDate = (DateTime)reader[3];
                    poco.IsSuccesful = reader.GetBoolean(4);                   
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log]
                                           WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                           SET 
                                               [Login] = @Login
                                              ,[Source_IP] = @Source_IP
                                              ,[Logon_Date] = @Logon_Date
                                              ,[Is_Succesful] = @Is_Succesful
                                         WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
