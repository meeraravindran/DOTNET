using Microsoft.AspNetCore.Mvc;
using Webapi.Models;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character Dany = new Character();
        [HttpGet]
        public ActionResult<Character> Get(){
            return Ok(Dany);
        }
    }
}