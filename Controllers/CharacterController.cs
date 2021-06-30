using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using System.Collections.Generic;
using System.Linq;
using Webapi.Services.CharacterService;
using System.Threading.Tasks;
using Webapi.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        // private static List<Character> characters = new List<Character>{
        //     new Character(),
        //     new Character{Id=1, Name = "Dany", Strength=34}
        // };
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharac(AddCharacterDto newCharac)
        {
            return Ok(await _characterService.AddCharacter(newCharac));

        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}