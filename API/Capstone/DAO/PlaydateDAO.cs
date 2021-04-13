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
        private const string SQL_GET_ALL_PLAYDATES = "select * from fullPlaydate";
        private const string SQL_GETPLAYDATEBYID = "select * from fullPlaydate where playdate_id = @playdate_id;";
        private const string SQL_ADDPLAYDATE = "insert into playdate (start_date_time, end_date_time user_id, location_id) values (@startDateTime, @endDateTime, @userId, @location_id); select @@IDENTITY;";
        private const string SQL_GET_PLAYDATES_BY_USERID = "select * from fullPlaydate where user_id = @userId;";
        //used to help builda fully featured playdate object
        private const string SQL_GET_PET_TYPES_PERMITTED_BY_PLAYDATE_ID = "select * from playdate_pet_type_permitted where playdate_id = @playdateId";
        private const string SQL_GET_PERSONALITIES_PERMITTED_BY_PLAYDATE_ID = "select * from playdate_personality_permitted where playdate_id = @playdateId";
        //these are for playdate filtering. They will need to be parameterized first before you can actually use them
        private const string SQL_GET_PLAYDATE_IDS_BY_PERMITTED_PET_TYPES_ARRAY = "select distinct playdate.playdate_id from playdate join playdate_pet_type_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((pet_type_id_is_permitted = 1) and (pet_type_id in ({0}))) or (-1 in ({0})))";
        private const string SQL_GET_PLAYDATE_IDS_BY_PROHIBITED_PET_TYPES_ARRAY = "select distinct playdate.playdate_id from playdate join playdate_pet_type_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((pet_type_id_is_permitted = 0) and (pet_type_id in ({0}))) or (-1 in ({0})))";
        private const string SQL_GET_PLAYDATE_IDS_BY_PERMITTED_PERSONALITIES_ARRAY = "select distinct playdate.playdate_id from playdate join playdate_personality_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((personality_id_is_permitted = 1) and(personality_id in ({0}))) or(-1 in ({0})))";
        private const string SQL_GET_PLAYDATE_IDS_BY_PROHIBITED_PERSONALITIES_ARRAY = "select distinct playdate.playdate_id from playdate join playdate_personality_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((personality_id_is_permitted = 0) and(personality_id in ({0}))) or(-1 in ({0})))";
        private const string SQL_GET_PLAYDATE_IDS_AND_DISTANCES_BY_DISTANCE_FROM_CENTER_POINT = "select playdate_id,distance_km,distance_mi from (select *, (distance_km * 0.62137)as distance_mi from (select *,dbo.Haversine_km(@centerLat,@centerLng,lat,lng) as distance_km from fullPlaydate)as km) as fullPlaydate_and_distance where( (distance_km <= @radius) or (@radius =-1) )";
        public PlaydateDAO(string connectionString)
        {
            this.connectionString = connectionString;
            this.petDAO = new PetDAO(connectionString);
        }

        public List<Playdate> GetPlaydates(PlaydateSearchFilter filter)
        {
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

                    #region filter on allowed personalities
                    ParameterizedSqlArray<int> allowedPersonalitiesArray = new ParameterizedSqlArray<int>(
                        $"and (playdate_id in ({SQL_GET_PLAYDATE_IDS_BY_PERMITTED_PERSONALITIES_ARRAY}))",
                        filter.allowedPersonalities,
                        "allowedPersonalities"
                        );
                    queryBuilder.Append(allowedPersonalitiesArray.Snippet);
                    cmd.Parameters.AddRange(allowedPersonalitiesArray.Parameters);

                    #endregion

                    #region filter on disallowed personalities
                    ParameterizedSqlArray<int> disallowedPersonalitiesArray = new ParameterizedSqlArray<int>(
                        $"and (playdate_id in ({SQL_GET_PLAYDATE_IDS_BY_PROHIBITED_PERSONALITIES_ARRAY}))",
                        filter.disallowedPersonalities,
                        "disallowedPersonalities"
                        );
                    queryBuilder.Append(disallowedPersonalitiesArray.Snippet);
                    cmd.Parameters.AddRange(disallowedPersonalitiesArray.Parameters);

                    #endregion

                    #region filter on allowed petTypes
                    ParameterizedSqlArray<int> allowedPetTypesArray = new ParameterizedSqlArray<int>(
                        $"and (playdate_id in ({SQL_GET_PLAYDATE_IDS_BY_PERMITTED_PET_TYPES_ARRAY}))",
                        filter.allowedPetTypes,
                        "allowedPetTypes"
                        );
                    queryBuilder.Append(allowedPetTypesArray.Snippet);
                    cmd.Parameters.AddRange(allowedPetTypesArray.Parameters);

                    #endregion

                    #region filter on disallowed petTypes
                    ParameterizedSqlArray<int> disallowedPetTypesArray = new ParameterizedSqlArray<int>(
                        $"and (playdate_id in ({SQL_GET_PLAYDATE_IDS_BY_PROHIBITED_PET_TYPES_ARRAY}))",
                        filter.disallowedPetTypes,
                        "disallowedPetTypes"
                        );
                    queryBuilder.Append(disallowedPetTypesArray.Snippet);
                    cmd.Parameters.AddRange(disallowedPetTypesArray.Parameters);

                    #endregion

                    #region filter by location
                    //todo: make this not explode
                    //queryBuilder.Append($"and (playdate_id in ({SQL_GET_PLAYDATE_IDS_AND_DISTANCES_BY_DISTANCE_FROM_CENTER_POINT}))");
                    cmd.Parameters.AddWithValue("@centerLat", filter.searchCenter.Lat);
                    cmd.Parameters.AddWithValue("@centerLng", filter.searchCenter.Lng);
                    cmd.Parameters.AddWithValue("@radius", filter.searchRadius);
                    //todo: add -1 override for no radius

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
                throw;
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

        //get petTypes permitted by playdateId
        public Dictionary<int, bool> GetPetTypesPermittedByPlaydateId(int playdateId)
        {
            Dictionary<int, bool> petTypesPermitted = new Dictionary<int, bool>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_PET_TYPES_PERMITTED_BY_PLAYDATE_ID, conn);
                    cmd.Parameters.AddWithValue("@playdateId", playdateId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int key = Convert.ToInt32(rdr["pet_type_id"]);
                        bool value = Convert.ToBoolean(rdr["pet_type_id_is_permitted"]);
                        petTypesPermitted[key] = value;
                    }

                }

            }
            catch (SqlException)
            {
                throw;
            }

            return petTypesPermitted;
        }

        //get personalities permitted by playdateId
        public Dictionary<int, bool> GetPersonalitiesPermittedByPlaydateId(int playdateId)
        {
            Dictionary<int, bool> personalitiesPermitted = new Dictionary<int, bool>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_PERSONALITIES_PERMITTED_BY_PLAYDATE_ID, conn);
                    cmd.Parameters.AddWithValue("@playdateId", playdateId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int key = Convert.ToInt32(rdr["personality_id"]);
                        bool value = Convert.ToBoolean(rdr["personality_id_is_permitted"]);
                        personalitiesPermitted[key] = value;
                    }

                }

            }
            catch (SqlException)
            {
                throw;
            }

            return personalitiesPermitted;
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
                Description = Convert.ToString(rdr["description"]),
                petTypesPermitted = GetPetTypesPermittedByPlaydateId(playdateId),
                personalitiesPermitted = GetPersonalitiesPermittedByPlaydateId(playdateId),
                Participants = petDAO.GetParticipantPetsByPlaydateId(playdateId)
            };


            return playdate;
        }

    }


}
