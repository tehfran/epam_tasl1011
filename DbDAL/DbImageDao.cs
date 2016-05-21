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
    public class DbImageDao : IImageDao
    {
        private string ConnectionString { get; set; }

        public DbImageDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public bool Add(Image image)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAward = connection.CreateCommand();
                cmdAddAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAward.CommandText = "Image_Add";
                cmdAddAward.Parameters.AddWithValue("@Id", image.Id);
                cmdAddAward.Parameters.AddWithValue("@Content", image.Content);
                cmdAddAward.Parameters.AddWithValue("@ContentType", image.ContentType);
                cmdAddAward.ExecuteNonQuery();
            }
            return true;
        }

        bool IImageDao.Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAward = connection.CreateCommand();
                cmdAddAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAward.CommandText = "Image_Delete";
                cmdAddAward.Parameters.AddWithValue("@Id", id);
                cmdAddAward.ExecuteNonQuery();
            }
            return true;
        }

        public Image Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAward = connection.CreateCommand();
                cmdAddAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAward.CommandText = "Image_Get";
                cmdAddAward.Parameters.AddWithValue("@Id", id);
                var query = cmdAddAward.ExecuteReader();
                query.Read();
                var result = new Image((int)query["id"], (byte[])query["content"], (string)query["contentType"]);
                query.Close();
                return result;
            }
        }

        public List<Image> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmdAddAward = connection.CreateCommand();
                cmdAddAward.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddAward.CommandText = "Image_GetAll";
                var query = cmdAddAward.ExecuteReader();
                var result = new List<Image>();
                while (query.Read())
                {
                    result.Add(new Image((int)query["id"], (byte[])query["content"], (string)query["contentType"]));
                }
                query.Close();
                return result;
            }
        }
    }
}
