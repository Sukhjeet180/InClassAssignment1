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
    public class SecurityLoginRepository : BaseAdo, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                               ([Id]
                                               ,[Login]
                                               ,[Password]
                                               ,[Created_Date]
                                               ,[Password_Update_Date]
                                               ,[Agreement_Accepted_Date]
                                               ,[Is_Locked]
                                               ,[Is_Inactive]
                                               ,[Email_Address]
                                               ,[Phone_Number]
                                               ,[Full_Name]
                                               ,[Force_Change_Password]
                                               ,[Prefferred_Language])
                                         VALUES
                                               (@Id
                                               ,@Login
                                               ,@Password
                                               ,@Created_Date
                                               ,@Password_Update_Date
                                               ,@Agreement_Accepted_Date
                                               ,@Is_Locked
                                               ,@Is_Inactive
                                               ,@Email_Address
                                               ,@Phone_Number
                                               ,@Full_Name
                                               ,@Force_Change_Password
                                               ,@Prefferred_Language)
                                    ";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@Created_Date", item.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", item.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT [Id]
                                          ,[Login]
                                          ,[Password]
                                          ,[Created_Date]
                                          ,[Password_Update_Date]
                                          ,[Agreement_Accepted_Date]
                                          ,[Is_Locked]
                                          ,[Is_Inactive]
                                          ,[Email_Address]
                                          ,[Phone_Number]
                                          ,[Full_Name]
                                          ,[Force_Change_Password]
                                          ,[Prefferred_Language]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Security_Logins]
                                    ";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityLoginPoco[] pocos = new SecurityLoginPoco[1000];
                int counter = 0;

                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetString(1);
                    poco.Password = reader.GetString(2);
                    poco.Created = (DateTime)reader[3];
                    poco.PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                    poco.AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    poco.PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9);
                    poco.FullName = reader.IsDBNull(10) ? null : reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    poco.PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12);
                    poco.TimeStamp = (byte[])reader[13];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                             WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Security_Logins]
                                               SET 
                                                   [Login] = @Login
                                                  ,[Password] = @Password
                                                  ,[Created_Date] = @Created_Date
                                                  ,[Password_Update_Date] = @Password_Update_Date
                                                  ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                                                  ,[Is_Locked] = @Is_Locked
                                                  ,[Is_Inactive] = @Is_Inactive
                                                  ,[Email_Address] = @Email_Address
                                                  ,[Phone_Number] = @Phone_Number
                                                  ,[Full_Name] = @Full_Name
                                                  ,[Force_Change_Password] = @Force_Change_Password
                                                  ,[Prefferred_Language] = @Prefferred_Language
                                             WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@Created_Date", item.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", item.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
