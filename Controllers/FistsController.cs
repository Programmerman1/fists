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
            ("Left", "Left Jr."),
            ("John", "Frerichs"),
            ("Tinder", "Grindr"),
            ("Smash U", "Burn U Up"),
            ("Bob", "Bob"),
            ("1", "2"),
        };

        // GET api/values
        [HttpGet]
        public ActionResult<dynamic> Get()
        {
            var myFists = fists[rng.Next(fists.Length)];
            return new {
                text = string.Format("Say hello to my friends {0} and {1}", myFists.Item1, myFists.Item2),
                response_type = "in_channel",
            };
        }

        // POST api/values
        [HttpPost]
        public ActionResult<dynamic> Post()
        {
            var myFists = fists[rng.Next(fists.Length)];
            return new {
                text = string.Format("Say hello to my friends {0} and {1}", myFists.Item1, myFists.Item2),
                response_type = "in_channel",
            };
        }
   }
}
