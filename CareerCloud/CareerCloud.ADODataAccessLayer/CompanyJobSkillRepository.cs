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
    public class CompanyJobSkillRepository : BaseAdo, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                               ([Id]
                                               ,[Job]
                                               ,[Skill]
                                               ,[Skill_Level]
                                               ,[Importance])
                                         VALUES
                                               (@Id
                                               ,@Job
                                               ,@Skill
                                               ,@Skill_Level
                                               ,@Importance)";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"
                                            SELECT [Id]
                                                  ,[Job]
                                                  ,[Skill]
                                                  ,[Skill_Level]
                                                  ,[Importance]
                                                  ,[Time_Stamp]
                                              FROM [dbo].[Company_Job_Skills]";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[7000];
                int counter = 0;

                while (reader.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.Importance = reader.GetInt32(4);
                   
                    poco.TimeStamp = (byte[])reader[5];
                    pocos[counter] = poco;
                    counter++;
                }

                conn.Close();
                return pocos.Where(pocos => pocos != null).ToList();

            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                             WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                           SET 
                                              [Job] = @Job
                                              ,[Skill] = @Skill
                                              ,[Skill_Level] = @Skill_Level
                                              ,[Importance] = @Importance
                                         WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);
                    conn.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
