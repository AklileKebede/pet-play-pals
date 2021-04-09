using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface IPetDAO
    {
        int AddPet(Pet petToAdd);
        int AddPetToUser(int petId, int userId);
        Dictionary<int, string> GetPersonalityTypes();
        Pet GetPetById(int petId);
        Dictionary<int, string> GetPetTypes();
        List<Pet> GetUserPets(int userId);
        bool OverwritePetPersonalityByPetId(int petId, int[] personalityIds);
        Pet RowToObject(SqlDataReader rdr);
        Pet UpdatePet(Pet petToUpdate);
    }
}