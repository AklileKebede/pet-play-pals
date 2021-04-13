﻿using Capstone.Controllers.searchfilters;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPlaydateDAO
    {
        int AddPlaydate(Playdate playdateToAdd);
        List<Playdate> GetAllPlaydates();
        Dictionary<int, bool> GetPersonalitiesPermittedByPlaydateId(int playdateId);
        Dictionary<int, bool> GetPetTypesPermittedByPlaydateId(int playdateId);
        Playdate GetPlaydateById(int playdateId);
        List<Playdate> GetPlaydates(PlaydateSearchFilter filter);
        List<Playdate> GetPlaydatesByUserId(int userId);
    }
}