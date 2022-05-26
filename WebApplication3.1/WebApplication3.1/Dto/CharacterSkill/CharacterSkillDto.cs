using WebApplication3._1.Models;

namespace WebApplication3._1.Dto.CharacterSkill
{
    public class CharacterSkillDto
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
