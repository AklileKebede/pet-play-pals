using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PlaydateDAO
    {
        private readonly string connectionString;
        private const string SQL_GETALLPLAYDATES = "select * from playdates;";
        private const string SQL_GETPLAYDATEBYID = "select * from playdates where playdate_id = @playdate_id;";
        private const string SQL_ADDPLAYDATE = "insert into playdates (date, location_id) values (@date, @location_id);";

        public PlaydateDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private Playdate RowToObject(SqlDataReader rdr)
        {
            Playdate playdate = new Playdate();
            playdate.PlaydateId = Convert.ToInt32(rdr["playdate_id"]);
            playdate.Date = Convert.ToDateTime(rdr["date"]);
            playdate.LocationId = Convert.ToInt32(rdr["location_id"]);

            return playdate;
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
                    SqlCommand cmd = new SqlCommand(SQL_GETALLPLAYDATES, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Playdate playdate = RowToObject(rdr);
                        playdates.Add(playdate);
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }

            return playdates;
        }

        //get a list of all playdates after given date?? 

        //get a specific playdate by id
        public Playdate GetPlaydateById(int playdateId)
        {
            Playdate playdate = new Playdate();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GETPLAYDATEBYID, conn);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        playdate = RowToObject(rdr);
                    }

                }

            }
            catch (SqlException)
            {
                throw;
            }

            return playdate;
        }

        //add a new playdate 
        public int AddPlaydate(Playdate playdateToAdd)
        {
            int playdateId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ADDPLAYDATE, conn);
                    cmd.Parameters.AddWithValue("@date", playdateToAdd.Date);
                    cmd.Parameters.AddWithValue("@location_id", playdateToAdd.LocationId);
                    playdateId = Convert.ToInt32(cmd.ExecuteScalar());
                    playdateToAdd.PlaydateId = playdateId;

                }

            }
            catch(SqlException)
            {
                throw;
            }


            return playdateId;

        }



    }
}
