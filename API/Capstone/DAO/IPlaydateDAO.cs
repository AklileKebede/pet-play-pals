using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPlaydateDAO
    {
        int AddPlaydate(Playdate playdateToAdd);
        List<Playdate> GetAllPlaydates();
        Playdate GetPlaydateById(int playdateId);
        List<Playdate> GetPlaydatesByUserId(int userId);
    }
}