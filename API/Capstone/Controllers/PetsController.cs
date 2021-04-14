﻿using Capstone.DAO;
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
    [Authorize]
    [ApiController]
    public class PetsController : AuthorizedControllerBase
    {
        private readonly IPetDAO petDAO;
        public PetsController(IPetDAO petDAO)
        {
            this.petDAO = petDAO;
        }
        [AllowAnonymous]
        [HttpGet("personalities")]
        public ActionResult<Dictionary<int, string>> getPersonalities()
        {
            Dictionary<int, string> personalities = petDAO.GetPersonalityTypes();
            return Ok(personalities);
        }
        [AllowAnonymous]
        [HttpGet("types")]
        public ActionResult<Dictionary<int, string>> getPetTypes()
        {
            Dictionary<int, string> petTypes = petDAO.GetPetTypes();
            return Ok(petTypes);
        }


        [HttpGet()]
        public ActionResult<List<Pet>> GetPets(int userId = -1)
        {

            List<Pet> pets = new List<Pet>();
            if (userId == -1)
            {
                pets = petDAO.GetAllPets();
            }
            else
            {
                pets = petDAO.GetPetsByUserId(userId);
            }
            if (pets == null)
            {
                return NoContent();
            }
            else return Ok(pets);
        }

        /// <summary>
        /// Adds a new pet to the database. the pet's ID will be automatically generated by the Database, 
        /// and the pet's userId (it's owner) will be the ID of the currently logged in user
        /// This should ensure that new pets are always owned by the person who entered them into the system
        /// </summary>
        /// <param name="petToAdd">a pet object to add to the database</param>
        /// <returns>that same pet object, but with it's petID and userID values populated</returns>

        [HttpPost()]
        public ActionResult<Pet> CreatePet(Pet petToAdd)
        {
            //first, add the id of the currently logged in user to petToAdd
            petToAdd.UserId = this.UserId;
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
            }
            else
            {
                Pet updatedPet = petDAO.UpdatePet(petToUpdate);
                return updatedPet;
            }
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> GetPetById(int petId)
        {
            Pet foundPet = petDAO.GetPetById(petId);
            if (foundPet == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(foundPet);
            }
        }

        //get pet by id

        //get all pets

        //get all pets by user ID

        //get pets by filter. query string

        //Adds a pet image url from Cloudnary into database
        [HttpPut("/image")]
        public ActionResult<Pet> AddPetImageURL(Pet pet)
        {
            int rowsAffected = petDAO.UpdatePetImageUrl(pet.PetId, pet.PetImageUrl);
            if (rowsAffected == 1)
            {
                Pet petWithUrl = petDAO.GetPetById(pet.PetId);
                return Ok(petWithUrl);
            }
            else return BadRequest();

          
        }


    }
}
