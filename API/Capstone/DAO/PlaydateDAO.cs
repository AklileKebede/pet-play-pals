using Capstone.Controllers.searchfilters;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PlaydateDAO : IPlaydateDAO
    {
        private readonly string connectionString;
        private IPetDAO petDAO;
        private const string SQL_GET_ALL_PLAYDATES = "select * from fullPlaydates";
        private const string SQL_GET_PLAYDATES_FILTERED = "select * from fullPlaydates where ((@userId = -1 OR user_id = @userId) and (@petTypeIds = null or ))";
        private const string SQL_GETPLAYDATEBYID = "select * from fullPlaydates where playdate_id = @playdate_id;";
        private const string SQL_ADDPLAYDATE = "insert into playdates (start_date_time, end_date_time user_id, location_id) values (@startDateTime, @endDateTime, @userId, @location_id); select @@IDENTITY;";
        private const string SQL_GET_PLAYDATES_BY_USERID = "select * from fullPlaydates where user_id = @userId;";

        public PlaydateDAO(string connectionString)
        {
            this.connectionString = connectionString;
            this.petDAO = new PetDAO(connectionString);
        }

        public List<Playdate> GetPlaydates(PlaydateSearchFilter filter)
        {

            //allowedPetTypes = allowedPetTypes ?? new int[] { -1 };
            //disallowedPetTypeIds = disallowedPetTypeIds ?? new int[] { -1 };

            List<Playdate> playdates = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    //Lets start building this insane SQL query
                    StringBuilder queryBuilder = new StringBuilder(SQL_GET_ALL_PLAYDATES);
                    //start the big ol' WHERE clause
                    queryBuilder.Append(" where(");
                    //filter on userId
                    queryBuilder.Append("(@userId = -1 OR user_id = @userId)");
                    cmd.Parameters.AddWithValue("@userId", filter.userId);

                    #region filter on requiredpetTypeID
                    string requiredPetTypesSnippett = " and (playdate_id in (select playdate_id from playdateIdsAndPetTypes where( (pet_type_id in ({0})) or (-1 in ({0})) ) ))";
                    /*this code is a workaround to functionality that really should be built into SQL.
                     * I.e. the ability to parameterize an array of values for use in an IN select statement
                     */
                    ParameterizedSqlArray<int> requiredPetTypesArray = new ParameterizedSqlArray<int>(
                        requiredPetTypesSnippett,
                        filter.requiredPetTypes,
                        "allowedPetTypeId");
                    queryBuilder.Append(requiredPetTypesArray.Snippet);
                    cmd.Parameters.AddRange(requiredPetTypesArray.Parameters);
                    #endregion

                    #region filter on disallowedpetTypeID

                    string disallowedPetTypesSnippett = "and (playdate_id not in (select playdate_id from playdateIdsAndPetTypes where( pet_type_id in ({0}))))";
                    ParameterizedSqlArray<int> disallowedPetTypesArray = new ParameterizedSqlArray<int>(
                        disallowedPetTypesSnippett,
                        filter.disallowedPetTypes,
                        "disallowedPetTypeId");
                    queryBuilder.Append(disallowedPetTypesArray.Snippet);
                    cmd.Parameters.AddRange(disallowedPetTypesArray.Parameters);
                    #endregion

                    #region filter on allowed personalities
                    string requiredPersonalitesSnippett = "";
                    ParameterizedSqlArray<int> requiredPersonalitesArray = new ParameterizedSqlArray<int>(
                        requiredPersonalitesSnippett,
                        filter.allowedPersonalities,
                        "allowedPersonalityId");
                    queryBuilder.Append(requiredPersonalitesArray.Snippet);
                    cmd.Parameters.AddRange(requiredPersonalitesArray.Parameters);

                    #endregion


                    //end the big ol' where clause
                    queryBuilder.Append(");");

                    cmd.CommandText = queryBuilder.ToString();

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
                    cmd.Parameters.AddWithValue("@startDateTime", playdateToAdd.StartDateTime);
                    cmd.Parameters.AddWithValue("@endDateTime", playdateToAdd.EndDateTime);
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
            int playdateId = Convert.ToInt32(rdr["playdate_id"]);
            Playdate playdate = new Playdate()
            {
                PlaydateId = playdateId,
                StartDateTime = Convert.ToDateTime(rdr["start_date_time"]),
                EndDateTime = Convert.ToDateTime(rdr["end_date_time"]),
                UserId = Convert.ToInt32(rdr["user_id"]),
                location = new Location()
                {
                    LocationId = Convert.ToInt32(rdr["location_id"]),
                    Name = Convert.ToString(rdr["location_name"]),
                    Address = Convert.ToString(rdr["address"]),
                    Lat = Convert.ToSingle(rdr["lat"]),
                    Lng = Convert.ToSingle(rdr["lng"])
                },
                Participants = petDAO.GetParticipantPetsByPlaydateId(playdateId)
            };


            return playdate;
        }

    }


}
