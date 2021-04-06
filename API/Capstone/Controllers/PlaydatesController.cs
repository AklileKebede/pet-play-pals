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
        private readonly IPlaydateDAO DAO;
        public PlaydatesController(IPlaydateDAO playdateDAO)
        {
            this.DAO = playdateDAO;
        }
        /// <summary>
        /// Gets all playdates that are in the database
        /// </summary>
        /// <returns>a list of all <see cref="Playdate"/> objects in the database</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Playdate>> getAllPlaydates()
        {
            List<Playdate> playdates = DAO.GetAllPlaydates();
            return Ok(playdates);
        }

        [HttpGet("{id}")]
        public ActionResult<Playdate> getPlaydateById(int id)
        {
            Playdate playdate = DAO.GetPlaydateById(id);
            if (playdate != null)
            {
                return Ok(playdate);
            } else
            {
                return NotFound();
            }
        }

        //add playdate
        [HttpPost()]
        public ActionResult<Playdate> CreatePlaydate(Playdate playdateToAdd)
        {
            int playdateId = DAO.AddPlaydate(playdateToAdd);
            if (playdateId < 1)
            {
                return BadRequest();
            }
            else return Ok(playdateToAdd);
        }
    }
}
