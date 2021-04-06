using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface IPetDAO
    {
        int AddPet(Pet petToAdd);
        Pet RowToObject(SqlDataReader rdr);
    }
}