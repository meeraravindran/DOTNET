using System.Collections.Generic;
using Webapi.Models;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Dtos.Character;
using AutoMapper;
using System;

namespace Webapi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id=1, Name = "Dany", Strength=34}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharac)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharac);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.Strength = updateCharacter.Strength;
                character.Defence = updateCharacter.Defence;
                character.IQ = updateCharacter.IQ;
                character.Class = updateCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.message = ex.Message;
                serviceResponse.Success = false;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = characters.First(c => c.Id == id);
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c=> _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.message = ex.Message;
                serviceResponse.Success = false;
            }


            return serviceResponse;
        }

    }
}