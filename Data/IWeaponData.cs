using System.Threading.Tasks;
//using Webapi.Migrations;
using Webapi.Models;

namespace Webapi.Data
{
    public interface IWeaponData
    {
        Task<Character> Insert(Weapon weapon);
        
    }
}