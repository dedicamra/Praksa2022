using Microsoft.EntityFrameworkCore;
using WebApplication3._1.Models;

namespace WebApplication3._1.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Weapon> Weapons{ get; set; }
        public DbSet<Skill> Skills{ get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //kompozitni key
            modelBuilder.Entity<CharacterSkill>().HasKey(x => new { x.CharacterId, x.SkillId });

            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Player");
        }
    }
}
