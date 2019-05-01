using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace fists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FistsController : ControllerBase
    {
        static Random rng = new Random();
        static (string, string)[] fists = new (string, string)[] {
            ("Jack Johnson", "Tom O'Leary"),
            ("LEFT", "RGHT"),
            ("Westside", "Eastside"),
            ("Mortar", "Pestle"),
            ("Ebony", "Ivory"),
        };

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var myFists = fists[rng.Next(fists.Length)];
            return string.Format("Say hello to my friends {0} and {1}", myFists.Item1, myFists.Item2);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post()
        {
            var myFists = fists[rng.Next(fists.Length)];
            return string.Format("Say hello to my friends {0} and {1}", myFists.Item1, myFists.Item2);
        }
   }
}
