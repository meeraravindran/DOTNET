using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Webapi.Models;
using Dapper;
using System.Data;
using AutoMapper;
namespace Webapi.Data
{
    public class WeaponData : IWeaponData
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WeaponData(IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;

        }
        public async Task<Character> Insert(Weapon weapon)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                await sqlConnection.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@characterId", weapon.CharacterId);
                param.Add("@userId", weapon.Id);

                var queryResult = sqlConnection.Query<Character>("Proc1", param, commandType: CommandType.StoredProcedure);
                Character character = _mapper.Map<Character>(queryResult);
                await sqlConnection.CloseAsync();
                return character;
            }

        }
    }
}