using System.Threading.Tasks;
using Webapi.Dtos.Character;
using Webapi.Dtos.Weapon;
using Webapi.Models;

namespace Webapi.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon (AddWeaponDto newWeapon);
    }
}