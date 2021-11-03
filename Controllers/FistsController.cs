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
        static System.Security.Cryptography.RandomNumberGenerator crng = new System.Security.Cryptography.RNGCryptoServiceProvider();
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
            ("SEND", "HELP"),
        };

        private string ReplaceRandomWithRandomString(string fist) {
            if ("random".Equals(fist, StringComparison.OrdinalIgnoreCase)) {
                var bytes = new byte[64];
                IEnumerable<byte> filteredBytes;
                do {
                    crng.GetBytes(bytes);
                    filteredBytes = bytes.Where(b => b > '\x20' && b < '\x7f' && b != '"' && b != '\\');
                } while (filteredBytes.Count() < 20);

                return new string(filteredBytes.Take(20).Select(b => (char)b).ToArray());
            }
            return fist;
        }

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
            fistOne = ReplaceRandomWithRandomString(fistOne);
            fistTwo = ReplaceRandomWithRandomString(fistTwo);
            return new {
                text = string.Format("Say hello to my friends", fistOne, fistTwo),
                response_type = "in_channel",
                attachments = new [] {
                    new {
                        fallback = string.Format("{0} and {1}", fistOne, fistTwo),
                        fields = new [] {
                            new {
                                title = "Left Fist 🤜",
                                value = fistOne,
                                @short = true,
                            },
                            new {
                                title = "🤛 Right Fist",
                                value = fistTwo,
                                @short = true,
                            },
                        },
                    }
                }
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

        [HttpPost("WebHook")]
        public async Task<ActionResult> WebHook([FromForm] string text) {
            string webhookUrl = Environment.GetEnvironmentVariable("WEBHOOKURL");
            
            string fists = Newtonsoft.Json.JsonConvert.SerializeObject(GetFists(text));

            var client = new System.Net.Http.HttpClient();
            await client.PostAsync(webhookUrl, new System.Net.Http.StringContent(fists));

            return NoContent();
        }
   }
}
