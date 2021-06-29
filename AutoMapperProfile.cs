using AutoMapper;
using Webapi.Dtos.Character;
using Webapi.Models;

namespace Webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}