using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.DbDAL
{
    public class DbLoginDao : ILoginDao
    {
        private string ConnectionString { get; set; }

        public DbLoginDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public Account Get(string login)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAccount = connection.CreateCommand();
                cmdGetAccount.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAccount.CommandText = "Account_Get";
                cmdGetAccount.Parameters.AddWithValue("@Login", login);
                var reader = cmdGetAccount.ExecuteReader();
                reader.Read();
                var account = new Account(((string)reader["login"])?.Trim(), ((string)reader["passhash"])?.Trim(), ((string)reader["rolename"])?.Trim());
                reader.Close();
                return account;
            }
        }

        public bool Add(Account acc)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAccount = connection.CreateCommand();
                cmdAddAccount.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAccount.CommandText = "Account_Add";
                cmdAddAccount.Parameters.AddWithValue("@Login", acc.Login);
                cmdAddAccount.Parameters.AddWithValue("@Passhash", acc.PassHash);
                cmdAddAccount.Parameters.AddWithValue("@RoleName", acc.Role);
                cmdAddAccount.ExecuteNonQuery();
            }
            return true;
        }

        public bool Edit(Account acc)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdEditAccount = connection.CreateCommand();
                cmdEditAccount.CommandType = System.Data.CommandType.StoredProcedure;
                cmdEditAccount.CommandText = "Account_Edit";
                cmdEditAccount.Parameters.AddWithValue("@Login", acc.Login);
                cmdEditAccount.Parameters.AddWithValue("@Passhash", acc.PassHash);
                cmdEditAccount.Parameters.AddWithValue("@RoleName", acc.Role);
                cmdEditAccount.ExecuteNonQuery();
            }
            return true;
        }

        public IEnumerable<Account> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAll = connection.CreateCommand();
                cmdGetAll.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAll.CommandText = "Account_GetAll";
                var reader = cmdGetAll.ExecuteReader();
                var result = new List<Account>();

                while (reader.Read())
                {
                    var account = new Account(((string)reader["login"]).Trim(), ((string)reader["passhash"]).Trim(), ((string)reader["rolename"]).Trim());
                    result.Add(account);
                }
                reader.Close();
                return result;
            }
        }

        public string[] GetAllRoles()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAllRoles = connection.CreateCommand();
                cmdGetAllRoles.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAllRoles.CommandText = "Account_GetAllRoles";
                var reader = cmdGetAllRoles.ExecuteReader();
                var result = new List<string>();
                while (reader.Read())
                {
                    var role = (string)reader["rolename"];
                    result.Add(role.Trim());
                }
                reader.Close();
                return result.ToArray();
            }
        }

        public bool ChangeRole(string login, string role)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdChangeRole = connection.CreateCommand();
                cmdChangeRole.CommandType = System.Data.CommandType.StoredProcedure;
                cmdChangeRole.CommandText = "Account_ChangeRole";
                cmdChangeRole.Parameters.AddWithValue("@Login", login);
                cmdChangeRole.Parameters.AddWithValue("@RoleName", role);
                cmdChangeRole.ExecuteNonQuery();
            }
            return true;
        }

        public bool AddRole(int id, string role)
        {
            throw new NotImplementedException();
        }
    }
}
