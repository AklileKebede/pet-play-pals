using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PlaydateDAO : IPlaydateDAO
    {
        private readonly string connectionString;
        private const string SQL_GET_ALL_PLAYDATES = "select * from fullPlaydates;";
        private const string SQL_GETPLAYDATEBYID = "select * from fullPlaydates where playdate_id = @playdate_id;";
        private const string SQL_ADDPLAYDATE = "insert into playdates (date, user_id, location_id) values (@date, @userId, @location_id); select @@IDENTITY;";
        private const string SQL_GET_PLAYDATES_BY_USERID = "select * from fullPlaydates where user_id = @userId;";

        public PlaydateDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }



        //get a list of all playdates 
        public List<Playdate> GetAllPlaydates()
        {
            List<Playdate> playdates = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_ALL_PLAYDATES, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Playdate playdate = RowToObject(rdr);
                        playdates.Add(playdate);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return playdates;
        }

        public List<Playdate> GetPlaydatesByUserId(int userId)
        {
            List<Playdate> playdates = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_PLAYDATES_BY_USERID, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Playdate playdate = RowToObject(rdr);
                        playdates.Add(playdate);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return playdates;
        }

        //get a list of all playdates after given date?? 

        //get a specific playdate by id
        public Playdate GetPlaydateById(int playdateId)
        {
            Playdate playdate = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GETPLAYDATEBYID, conn);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        playdate = RowToObject(rdr);
                    }

                }

            }
            catch (SqlException)
            {
                return playdate;
            }

            return playdate;
        }

        //add a new playdate 
        public int AddPlaydate(Playdate playdateToAdd)
        {
            int playdateId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ADDPLAYDATE, conn);
                    cmd.Parameters.AddWithValue("@date", playdateToAdd.Date);
                    cmd.Parameters.AddWithValue("@userId", playdateToAdd.UserId);
                    cmd.Parameters.AddWithValue("@location_id", playdateToAdd.location.LocationId);
                    playdateId = Convert.ToInt32(cmd.ExecuteScalar());
                    playdateToAdd.PlaydateId = playdateId;

                }
            }
            catch (SqlException)
            {
                return playdateId;
            }

            return playdateId;

        }


        private Playdate RowToObject(SqlDataReader rdr)
        {
            Playdate playdate = new Playdate()
            {
                PlaydateId = Convert.ToInt32(rdr["playdate_id"]),
                Date = Convert.ToDateTime(rdr["date"]),
                UserId = Convert.ToInt32(rdr["user_id"]),
                location = new Location()
                {
                    LocationId = Convert.ToInt32(rdr["location_id"]),
                    Name = Convert.ToString(rdr["location_name"]),
                    Address = Convert.ToString(rdr["address"]),
                    Lat = Convert.ToSingle(rdr["lat"]),
                    Lng = Convert.ToSingle(rdr["lng"])
                }
            };


            return playdate;
        }



    }
}
