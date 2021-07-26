using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Webapi.Models;
using Dapper;
using System.Data;
using AutoMapper;

namespace Webapi.Data
{
    public class CharacterData : ICharacterData
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CharacterData(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<Character> GetCharacter(int characterId, int userId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                await sqlConnection.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@characterId", characterId);
                param.Add("@userId", userId);

                var queryResult = sqlConnection.Query<Character>("Proc1", param, commandType: CommandType.StoredProcedure);
                Character character = _mapper.Map<Character>(queryResult);
                await sqlConnection.CloseAsync();
                return character;
            }

        }
    }
}