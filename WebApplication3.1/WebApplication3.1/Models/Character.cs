using System.Collections.Generic;

namespace WebApplication3._1.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

        public User User { get; set; }
        public int UserId { get; set; }
        public virtual Weapon Weapon { get; set; }
        public virtual List<CharacterSkill> CharacterSkills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
