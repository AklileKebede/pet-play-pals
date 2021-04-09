using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PetsController : AuthorizedControllerBase
    {
        private readonly IPetDAO petDAO;
        public PetsController(IPetDAO petDAO)
        {
            this.petDAO = petDAO;
        }

        [HttpGet("personalities")]
        public ActionResult<Dictionary<int,string>> getPersonalities()
        {
            Dictionary<int, string> personalities = petDAO.GetPersonalityTypes();
            return Ok(personalities);
        }

        [HttpGet("types")]
        public ActionResult<Dictionary<int, string>> getPetTypes()
        {
            Dictionary<int, string> petTypes = petDAO.GetPetTypes();
            return Ok(petTypes);
        }


        [HttpGet()]
        public ActionResult<List<Pet>> UsersPets(int userId)
        {
            List<Pet> pets = petDAO.GetUserPets(userId);
            if (pets == null)
            {
                return NoContent();
            }
            else return Ok(pets);
        }

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

        [HttpPut("{petId}")]
        public ActionResult<Pet> UpdatePet(int petId, Pet petToUpdate)
        {
            //todo: if the pet to update is not owned by the current login user, return 403
            if (petId != petToUpdate.PetId)
            {
                return BadRequest("The PetId and the id of petToUpdate do not match.");
            } else
            {
                Pet updatedPet = petDAO.UpdatePet(petToUpdate);
                return updatedPet;
            }
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> GetPetById(int petId)
        {
            Pet foundPet = petDAO.GetPetById(petId);
            if(foundPet == null)
            {
                return NotFound();
            } else
            {
                return Ok(foundPet);
            }
        }

        //get pet by id

        //get all pets

        //get all pets by user ID

        //get pets by filter. query string




    }
}
