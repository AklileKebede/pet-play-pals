using Capstone.Models;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PetDAO : IPetDAO
    {
        private readonly string connectionString;
        private const string SQL_ADDPET = "insert into pets(pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (@pet_name, @birthday, @sex, @pet_type_id, @pet_breed, @color, @bio); select @@IDENTITY;";
        private const string SQL_ADDPETTOUSER = "insert into user_pet(user_id, pet_id) Values(@userId, @petId); select @@IDENTITY";
        private const string SQL_GETUSERPET = "select * from fullPets p join user_pet u_p on u_p.pet_id = p.pet_id where u_p.user_id = @userId";
        private const string SQL_GETALLPERSONALITIES = "select * from personality";
        private const string SQL_GETPERSONALITIESFORPETBYID = "select personality_name,personality.personality_id from personality join personality_pet on personality.personality_id = personality_pet.personality_id where personality_pet.pet_id = @petId";
        private const string SQL_GETALLPETTYPES = "select * from pet_types";
        private const string SQL_UPDATE_PET_BY_ID_WITH_PERSONALITIES = "begin transaction; update pets set pet_name = @petName, birthday = @birthday, sex = @sex, pet_type_id = @petTypeId, pet_breed = @petBreed, color = @color, bio = @bio; delete from personality_pet where pet_id = @petId; insert into personality_pet select @petId, personality_id from personality where personality_id in ({0}); commit transaction;";
        private const string SQL_GET_PET_BY_ID = "select * from fullPets where pet_id = @petId";


        public PetDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }


        /// <summary>Gets a dictionary of valid personality types.</summary>
        /// <returns> a <see cref="Dictionary{int, String}"/>
        /// </returns>
        public Dictionary<int, string> GetPersonalityTypes()
        {
            Dictionary<int, string> personalities = new Dictionary<int, string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GETALLPERSONALITIES, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = Convert.ToInt32(rdr["personality_id"]);
                        string name = Convert.ToString(rdr["personality_name"]);
                        personalities[id] = name;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return personalities;
        }

        /// <summary>
        /// Gets a list of all valid pet types and their IDs
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetPetTypes()
        {
            Dictionary<int, string> petTypes = new Dictionary<int, string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GETALLPETTYPES, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = Convert.ToInt32(rdr["pet_type_id"]);
                        string name = Convert.ToString(rdr["pet_type_name"]);
                        petTypes[id] = name;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return petTypes;
        }

        public int AddPet(Pet petToAdd)
        {
            int petId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ADDPET, conn);
                    cmd.Parameters.AddWithValue("@pet_name", petToAdd.PetName);
                    cmd.Parameters.AddWithValue("@birthday", petToAdd.Birthday);
                    cmd.Parameters.AddWithValue("@sex", petToAdd.Sex);
                    cmd.Parameters.AddWithValue("@pet_type_id", petToAdd.PetType);
                    cmd.Parameters.AddWithValue("@pet_breed", petToAdd.Breed);
                    cmd.Parameters.AddWithValue("@color", petToAdd.Color);
                    cmd.Parameters.AddWithValue("@bio", petToAdd.Bio);
                    petId = Convert.ToInt32(cmd.ExecuteScalar());
                    petToAdd.PetId = petId;

                }
            }
            catch (SqlException)
            {
                return petId;
            }

            return petId;
        }

        public int AddPetToUser(int petId, int userId)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ADDPETTOUSER, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@petId", petId);
                    rowsAffected = cmd.ExecuteNonQuery();

                }

            }
            catch (SqlException)
            {
                return rowsAffected;
            }
            return rowsAffected;

        }

        public Pet GetPetById(int petId)
        {
            Pet pet = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GET_PET_BY_ID, conn);
                    cmd.Parameters.AddWithValue("@petId", petId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        pet = RowToObject(rdr);
                    }
                    
                }
            }
            catch (SqlException)
            {
                throw;
            }


            return pet;
        }

        //edit a pet
        public Pet UpdatePet(Pet petToUpdate)
        {
            Pet updatedPet = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@petId", petToUpdate.PetId);
                    cmd.Parameters.AddWithValue("@petName", petToUpdate.PetName);
                    cmd.Parameters.AddWithValue("@birthday", petToUpdate.Birthday);
                    cmd.Parameters.AddWithValue("@sex", petToUpdate.Sex);
                    cmd.Parameters.AddWithValue("@petTypeId", petToUpdate.PetTypeId);
                    cmd.Parameters.AddWithValue("@petBreed", petToUpdate.Breed);
                    cmd.Parameters.AddWithValue("@color", petToUpdate.Color);
                    cmd.Parameters.AddWithValue("@bio", petToUpdate.Bio);

                    //here we will add 1-by-1 a list of personality IDs to give to the updated Pet

                    //this will hold {"@personalityId0","@personalityId1","@personalityId2"....} as many personalities as there are in the petToUpdate
                    List<string> personalityIdParamNames = new List<string>();
                    int i = 0;
                    foreach (int personalityId in petToUpdate.PersonalityIds)
                    {
                        string paramName = $"@personalityId{i}";
                        cmd.Parameters.AddWithValue(paramName, personalityId);
                        personalityIdParamNames.Add(paramName);
                        i++;
                    }
                    cmd.CommandText = String.Format(SQL_UPDATE_PET_BY_ID_WITH_PERSONALITIES, string.Join(",", personalityIdParamNames));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        updatedPet = GetPetById(petToUpdate.PetId);
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }

            return updatedPet;
        }


        // get all pets for a registered user 
        public List<Pet> GetUserPets(int userId)
        {

            List<Pet> usersPets = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GETUSERPET, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Pet pet = RowToObject(rdr);
                        usersPets.Add(pet);
                    }

                }

            }
            catch (SqlException)
            {
                return usersPets;
            }
            return usersPets;


        }
        public Dictionary<int, string> GetPersonalitiesByPetId(int petId)
        {
            Dictionary<int, string> personalities = new Dictionary<int, string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GETPERSONALITIESFORPETBYID, conn);
                    cmd.Parameters.AddWithValue("@petId", petId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        personalities[Convert.ToInt32(rdr["personality_id"])] = (Convert.ToString(rdr["personality_name"]));
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }

            return personalities;
        }

        public Pet RowToObject(SqlDataReader rdr)
        {
            Pet pet = new Pet();
            pet.PetId = Convert.ToInt32(rdr["pet_id"]);
            pet.PetName = Convert.ToString(rdr["pet_name"]);
            pet.Birthday = Convert.ToDateTime(rdr["birthday"]);
            pet.Sex = Convert.ToChar(rdr["sex"]);
            pet.PetTypeId = Convert.ToInt32(rdr["pet_type_id"]);
            pet.PetType = Convert.ToString(rdr["pet_type_name"]);
            pet.Breed = Convert.ToString(rdr["pet_breed"]);
            pet.Color = Convert.ToString(rdr["color"]);
            pet.Bio = Convert.ToString(rdr["bio"]);
            Dictionary<int, string> Personalities = GetPersonalitiesByPetId(pet.PetId);
            pet.Personalities = Personalities.Values.ToArray();
            pet.PersonalityIds = Personalities.Keys.ToArray();

            return pet;
        }

    }
}
