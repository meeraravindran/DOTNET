using System.Collections.Generic;

namespace Webapi.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Damage { get; set; }
        public List<Character> Characters { get; set; }
    }
}