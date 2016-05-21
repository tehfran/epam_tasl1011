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
    public class DbAwardDao : IAwardDao
    {
        private string ConnectionString { get; set; }

        public DbAwardDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public bool Add(Award award)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAward = connection.CreateCommand();
                cmdAddAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAward.CommandText = "Award_Add";
                cmdAddAward.Parameters.AddWithValue("@Id", award.Id);
                cmdAddAward.Parameters.AddWithValue("@Name", award.Name);
                cmdAddAward.Parameters.AddWithValue("@ImageId", (object)award.Image ?? DBNull.Value);
                cmdAddAward.ExecuteNonQuery();
            }
            return true;
        }

        public bool AddImage(int awardId, int? imageId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAwardImage = connection.CreateCommand();
                cmdAddAwardImage.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAwardImage.CommandText = "Award_AddImage";
                cmdAddAwardImage.Parameters.AddWithValue("@Id", awardId);
                cmdAddAwardImage.Parameters.AddWithValue("@ImageId", imageId);
                cmdAddAwardImage.ExecuteNonQuery();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdDeleteAward = connection.CreateCommand();
                cmdDeleteAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdDeleteAward.CommandText = "Award_Delete";
                cmdDeleteAward.Parameters.AddWithValue("@Id", id);
                cmdDeleteAward.ExecuteNonQuery();
            }
            return true;
        }

        public bool Edit(Award award)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdEditAward = connection.CreateCommand();
                cmdEditAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdEditAward.CommandText = "Award_Edit";
                cmdEditAward.Parameters.AddWithValue("@Id", award.Id);
                cmdEditAward.Parameters.AddWithValue("@Name", award.Name);
                cmdEditAward.Parameters.AddWithValue("@ImageId", (object)award.Image ?? DBNull.Value);
                cmdEditAward.ExecuteNonQuery();
            }
            return true;
        }

        public Award Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAward = connection.CreateCommand();
                cmdGetAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAward.CommandText = "Award_Get";
                cmdGetAward.Parameters.AddWithValue("@Id", id);
                var query = cmdGetAward.ExecuteReader();
                query.Read();
                var result = new Award((int)query["id"], (string)query["name"], query["imageId"] as int?);
                query.Close();
                return result;
            }
        }

        public IEnumerable<Award> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdGetAllAwards = connection.CreateCommand();
                cmdGetAllAwards.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAllAwards.CommandText = "Award_GetAll";
                var query = cmdGetAllAwards.ExecuteReader();
                var result = new List<Award>();
                while (query.Read())
                {
                    result.Add(new Award((int)query["id"], (string)query["name"], query["imageId"] as int?));
                }
                query.Close();
                return result;
            }
        }
    }
}
