using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Dtos.Character;
using Webapi.Dtos.Weapon;
using Webapi.Models;

namespace Webapi.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
       // private readonly ICharacterData _characterData;

        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            //_characterData = characterData;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId 
                && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    response.Success = false;
                    response.message = "Character not found";
                    return response;
                }
                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                 response.Success = false;
                 response.message = ex.Message;
            }
            // try
            // {
            //     int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //     var character = await _characterData.GetCharacter(newWeapon.CharacterId, userId);
            //     if (character == null)
            //     {
            //         response.Success = false;
            //         response.message = "Character not found";
            //         return response;
            //     }
            //     var weapon = new Weapon
            //     {
            //         Name = newWeapon.Name,
            //         Damage = newWeapon.Damage,
            //         Character = character
            //     };
            // }
            // catch (Exception ex)
            // {
            //     response.Success = false;
            //     response.message = ex.Message;
            // }
            return response;
        }
    }
}