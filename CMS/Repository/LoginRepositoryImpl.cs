using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CMS.Repository
{
          public class LoginRepositoryImpl : ILoginRepository

        {
            //Connection String

            private readonly string connString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            public async Task<int> GetRoleIdAsync(string userName, string password)
            {
                int roleId = 0;
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand("sp_LoginUser", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", userName);
                        command.Parameters.AddWithValue("@Password", password);

                        SqlParameter roleIdParam = new SqlParameter("@RoleId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };


                        command.Parameters.Add(roleIdParam);
                        await command.ExecuteNonQueryAsync();

                        if (command.Parameters["@RoleId"].Value != DBNull.Value)
                        {
                            roleId = Convert.ToInt32(roleIdParam.Value);
                        }


                        return roleId;
                    }
                }

            }
        }
    }
