using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PetController : AuthorizedControllerBase
    {
        private readonly IPetDAO petDAO;
        public PetController(IPetDAO petDAO)
        {
            this.petDAO = petDAO;
        }

        //[HttpGet("{id}")]
        //public ActionResult<List<Playdate> getUsersPets(int id)
        //{
        //     List<Pet> pets = petDao.get(id);
        //    if (playdate != null)
        //    {
        //        return Ok(playdate);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost()]
        public ActionResult<Pet> CreatePet(Pet petToAdd)
        {
            int petId = petDAO.AddPet(petToAdd);
            if (petId < 1)
            {
                return BadRequest();
            }
            else
            {
                petToAdd.PetId = petId;
                return Ok(petToAdd);
            }
        }

        //get pet by id

        //get all pets

        //get all pets by user ID

        //get pets by filter. query string




    }
}
