using Webapi.Models;

namespace Webapi.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Meera";
        public int IQ { get; set; } = 100;
        public int Strength { get; set; } = 90;
        public int Defence { get; set; } = 95;
        public RpgClass Class { get; set; } = RpgClass.Queen;
    }
}