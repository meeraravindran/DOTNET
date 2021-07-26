using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Data
{
    public interface ICharacterData
    {
        Task<Character> GetCharacter(int characterId, int userId);
    }
}