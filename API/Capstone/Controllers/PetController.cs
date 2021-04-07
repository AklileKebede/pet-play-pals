using Capstone.DAO;
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




    }
}
