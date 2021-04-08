using Capstone.Models;
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
        private const string SQL_GETUSERPET = "select * from pets p join user_pet u_p on u_p.pet_id = p.pet_id where u_p.user_id = @userId";

        public PetDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Pet RowToObject(SqlDataReader rdr)
        {
            Pet pet = new Pet();
            pet.PetName = Convert.ToString(rdr["pet_name"]);
            pet.Birthday = Convert.ToDateTime(rdr["birthday"]);
            pet.Sex = Convert.ToChar(rdr["sex"]);
            pet.PetType = (PetType)Convert.ToInt32(rdr["pet_type_id"]);
            pet.Breed = Convert.ToString(rdr["pet_breed"]);
            pet.Color = Convert.ToString(rdr["color"]);
            pet.Bio = Convert.ToString(rdr["bio"]);

            return pet;
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

        // get all pets for a registered user 


    }
}
