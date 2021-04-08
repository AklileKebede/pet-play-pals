using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface IPetDAO
    {
        int AddPet(Pet petToAdd);
        int AddPetToUser(int petId, int userId);
        List<Pet> GetUserPets(int userId);
        Pet RowToObject(SqlDataReader rdr);
    }
}