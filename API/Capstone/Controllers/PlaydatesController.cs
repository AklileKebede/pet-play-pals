using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaydatesController : AuthorizedControllerBase
    {
        private readonly IPlaydateDAO playdateDao;
        private readonly ILocationDAO locationDao;
        public PlaydatesController(IPlaydateDAO playdateDAO, ILocationDAO locationDAO)
        {
            this.playdateDao = playdateDAO;
            this.locationDao = locationDAO;
        }
        /// <summary>
        /// Gets a list of playdates that meet the search criteria. If a search criteria is left blank, it it not used.
        /// </summary>
        /// <returns>a list of all <see cref="Playdate"/> objects in the database</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Playdate>> getPlaydates(int userId = -1)
        {
            List<Playdate> playdates = new List<Playdate>();
            if (userId == -1)
            {
                playdates = playdateDao.GetAllPlaydates(); 
            }
            else
            {
                playdates = playdateDao.GetPlaydatesByUserId(userId);
            }
            if (playdates == null)
            {
                return NoContent();
            }
            return Ok(playdates);
        }

        /// <summary>
        /// Gets a playdate by ID
        /// </summary>
        /// <param name="id">The Id of the playdate to get</param>
        /// <returns>a <see cref="Playdate"/> object</returns>
        [HttpGet("{id}")]
        public ActionResult<Playdate> getPlaydateById(int id)
        {
            Playdate playdate = playdateDao.GetPlaydateById(id);
            if (playdate != null)
            {
                return Ok(playdate);
            }
            else
            {
                return NoContent();
            }
        }

        //add playdate
        [HttpPost()]
        public ActionResult<Playdate> CreatePlaydate(Playdate playdateToAdd)
        {

            //if a location ID was not specified, but location data was, we need to check if a similar location already exists in the DB
            if (playdateToAdd.location.LocationId == -1)
            {
                //lets look for a similar location
                int LocationIdOnDB = locationDao.GetIdByLocation(playdateToAdd.location);
                if (LocationIdOnDB == -1)
                {
                    //this means that the location is NOT in the database, so we must add it
                    LocationIdOnDB = locationDao.AddLocation(playdateToAdd.location);
                }
                playdateToAdd.location.LocationId = LocationIdOnDB;

            }
            //now the location of the playdate should be properly in the DB

            int playdateId = playdateDao.AddPlaydate(playdateToAdd);
            if (playdateId < 1)
            {
                return StatusCode(500, "Something went wrong when trying to add your new playdate!");
            }
            else return Ok(playdateToAdd);
        }
    }
}
