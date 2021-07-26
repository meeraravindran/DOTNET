using AutoMapper;
using Webapi.Dtos.Character;
using Webapi.Dtos.Skill;
using Webapi.Dtos.Weapon;
using Webapi.Models;

namespace Webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}