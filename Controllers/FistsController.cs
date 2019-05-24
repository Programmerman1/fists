﻿using System;
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

        private dynamic GetFists(string text) {
            text = text ?? "";
            string fistOne = null, fistTwo = null;
            if (text.Contains(',')) {
                fistOne = text.Substring(0, text.IndexOf(',')).Trim();
                fistTwo = text.Substring(text.IndexOf(',') + 1).Trim();
            } else if (text.Contains(" and ")) {
                fistOne = text.Substring(0, text.IndexOf(" and ")).Trim();
                fistTwo = text.Substring(text.IndexOf(" and ") + 5).Trim();
            } else {
                (fistOne, fistTwo) = fists[rng.Next(fists.Length)];
            }
            return new {
                text = string.Format("Say hello to my friends {0} and {1}", fistOne, fistTwo),
                response_type = "in_channel",
            };
        }

        // GET api/values
        [HttpGet]
        public ActionResult<dynamic> Get(string text)
        {
            return GetFists(text);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<dynamic> Post([FromForm]string text)
        {
            return GetFists(text);
        }
   }
}
