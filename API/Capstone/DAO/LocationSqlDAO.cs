using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class LocationSqlDAO : ILocationDAO
    {
        private const string SQL_GET_ALL_LOCATIONS = "select * from locations";
        private readonly string connectionString;
        public LocationSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Gets a list of all locations in the database.
        /// </summary>
        /// <returns>a list of <see cref="Location">Locations</see> </returns>
        public List<Location> GetAllLocations()
        {
            List<Location> locationsToReturn = new List<Location>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_ALL_LOCATIONS, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        locationsToReturn.Add(rowToObject(rdr));
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }
            return locationsToReturn;
        }


        /// <summary>
        /// Gets a location object by a given locationId.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns> a <see cref="Location"/> object for a given locationID, or null of no such location is found</returns>
        public Location GetLocationById(int locationId)
        {
            Location location = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from locations where location_id = @locationId", conn);
                    cmd.Parameters.AddWithValue("@locationId", locationId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        location = (rowToObject(rdr));
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }
            return location;
        }

        private Location rowToObject(SqlDataReader rdr)
        {
            Location location = new Location()
            {
                LocationId = Convert.ToInt32(rdr["location_id"]),
                Name = Convert.ToString(rdr["name"]),
                Address = Convert.ToString(rdr["address"]),
                Lat = Convert.ToSingle(rdr["lat"]),
                Lng = Convert.ToSingle(rdr["lng"])

            };
            return location;
        }
    }
}
