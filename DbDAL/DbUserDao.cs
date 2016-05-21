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
    public class DbUserDao : IUserDao
    {
        private string ConnectionString { get; set; }

        public DbUserDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public bool Add(User user)
        {
            using(var connection = new SqlConnection(ConnectionString)){
                connection.Open();
                var cmdAddUser = connection.CreateCommand();
                cmdAddUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddUser.CommandText = "User_Add";
                cmdAddUser.Parameters.AddWithValue("@Id", user.Id);
                cmdAddUser.Parameters.AddWithValue("@Username", user.Username);
                cmdAddUser.Parameters.AddWithValue("@Birthdate", user.BirthDate);
                cmdAddUser.Parameters.AddWithValue("@ImageId", (object)user.Image ?? DBNull.Value);
                cmdAddUser.ExecuteNonQuery();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdDeleteUser = connection.CreateCommand();
                cmdDeleteUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdDeleteUser.CommandText = "User_Delete";
                cmdDeleteUser.Parameters.AddWithValue("Id", id);
                cmdDeleteUser.ExecuteNonQuery();
            }
            return true;
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetUser = connection.CreateCommand();
                cmdGetUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetUser.CommandText = "User_Get";
                cmdGetUser.Parameters.AddWithValue("@Id", id);
                var result = cmdGetUser.ExecuteReader();
                result.Read();
                var user = new User((int)result["id"], (string)result["username"], (DateTime)result["birthdate"]);
                user.Image = result["imageId"] as int?;
                user.Awards = GetUserAwards(user.Id);
                result.Close();
                return user;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAllUsers = connection.CreateCommand();
                cmdGetAllUsers.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAllUsers.CommandText = "User_GetAll";
                var result = cmdGetAllUsers.ExecuteReader();
                var users = new List<User>();
                while (result.Read())
                {
                    var user = new User((int)result["id"], (string)result["username"], (DateTime)result["birthdate"]);
                    user.Image = result["imageId"] as int?;
                    user.Awards = GetUserAwards(user.Id);
                    users.Add(user);
                }
                result.Close();
                return users;
            }
        }

        public bool Rescind(int userId, int awardId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdRescindAward = connection.CreateCommand();
                cmdRescindAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdRescindAward.CommandText = "User_RescindAward";
                cmdRescindAward.Parameters.AddWithValue("@UserId", userId);
                cmdRescindAward.Parameters.AddWithValue("@AwardId", awardId);
                cmdRescindAward.ExecuteNonQuery();
            }
            return true;
        }

        public bool Grant(int userId, int awardId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGrantAward = connection.CreateCommand();
                cmdGrantAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGrantAward.CommandText = "User_GrantAward";
                cmdGrantAward.Parameters.AddWithValue("@UserId", userId);
                cmdGrantAward.Parameters.AddWithValue("@AwardId", awardId);
                cmdGrantAward.ExecuteNonQuery();
            }
            return true;
        }

        public bool Edit(User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdEditUser = connection.CreateCommand();
                cmdEditUser.CommandText = "User_Edit";
                cmdEditUser.Parameters.AddWithValue("@Id", user.Id);
                cmdEditUser.Parameters.AddWithValue("@Username", user.Username);
                cmdEditUser.Parameters.AddWithValue("@Birthdate", user.BirthDate);
                cmdEditUser.Parameters.AddWithValue("@ImageId", (object)user.Image ?? DBNull.Value);
                cmdEditUser.ExecuteNonQuery();
            }
            return true;
        }

        public bool AddImage(int userId, int? imageId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddImage = connection.CreateCommand();
                cmdAddImage.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddImage.CommandText = "User_AddImage";
                cmdAddImage.Parameters.AddWithValue("@UserId", userId);
                cmdAddImage.Parameters.AddWithValue("@ImageId", imageId);
                cmdAddImage.ExecuteNonQuery();
            }
            return true;
        }

        private HashSet<int> GetUserAwards(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetUserAwards = connection.CreateCommand();
                cmdGetUserAwards.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetUserAwards.CommandText = "User_GetAwards";
                cmdGetUserAwards.Parameters.AddWithValue("@Id", userId);
                var query = cmdGetUserAwards.ExecuteReader();
                var result = new HashSet<int>();
                while (query.Read())
                {
                    result.Add((int)query["awardid"]);
                }
                query.Close();
                return result;
            }
        }
    }
}
