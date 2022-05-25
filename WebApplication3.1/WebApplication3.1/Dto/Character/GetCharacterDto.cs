using System.Collections.Generic;
//using WebApplication3._1.Dtos.Skills;
//using WebApplication3._1.Dtos.Weapon;
using WebApplication3._1.Models;

namespace WebApplication3._1.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
       // public GetWeaponDto Weapon { get; set; }
       // public List<GetSkillDto> Skills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
