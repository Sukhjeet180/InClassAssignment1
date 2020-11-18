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
    public class SystemLanguageCodeRepository : BaseAdo, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                               ([LanguageID]
                                               ,[Name]
                                               ,[Native_Name])
                                         VALUES
                                               (@LanguageID
                                               ,@Name
                                               ,@Native_Name)";
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Native_Name", item.NativeName);
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [LanguageID]
                                          ,[Name]
                                          ,[Native_Name]
                                      FROM [dbo].[System_Language_Codes]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);                    
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                             WHERE [LanguageID] = @LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);                    
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                               SET 
                                                   [Name] = @Name
                                                  ,[Native_Name] = @Native_Name
                                             WHERE [LanguageID] = @LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Native_Name", item.NativeName);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
