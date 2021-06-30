using System.Collections.Generic;
using Webapi.Models;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Dtos.Character;
using AutoMapper;
using System;
using Webapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Webapi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharac)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharac);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
            .Where(c => c.User.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            if (dbCharacters != null)
            {
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.message = "No characters found";
                return serviceResponse;
            }

        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    character.Name = updateCharacter.Name;
                    character.Strength = updateCharacter.Strength;
                    character.Defence = updateCharacter.Defence;
                    character.IQ = updateCharacter.IQ;
                    character.Class = updateCharacter.Class;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.message = "Character not found";
                }

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
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Characters.Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.message = "Character not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.message = ex.Message;
                serviceResponse.Success = false;
            }


            return serviceResponse;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    }
}