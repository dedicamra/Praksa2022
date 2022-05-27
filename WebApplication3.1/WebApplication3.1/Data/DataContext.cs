using Microsoft.EntityFrameworkCore;
using WebApplication3._1.Models;

namespace WebApplication3._1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //kompozitni key
            modelBuilder.Entity<CharacterSkill>().HasKey(x => new { x.CharacterId, x.SkillId });

            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Player");


            //DataSeeding
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 30 },
                new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
                new Skill
                {
                    Id = 3,
                    Name = "Blizzard",
                    Damage = 50
                });

            Utility.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Username = "user1" },
                new User { Id = 2, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Username = "user2" }
                );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id=1, 
                    Name="Frodo",
                    Class=RpgClass.Knight,
                    HitPoints=100,
                    Strength=15,
                    Defence=10,
                    Intelligence=10,
                    UserId=1
                },
                new Character
                {
                    Id = 2,
                    Name = "Sam",
                    Class = RpgClass.Mage,
                    HitPoints = 100,
                    Strength = 11,
                    Defence = 8,
                    Intelligence = 20,
                    UserId = 2
                }
                );

            modelBuilder.Entity<Weapon>().HasData(
                new Weapon { Id=1, Name="Weapon1", Damage=20, CharacterId=1},
                new Weapon { Id=2, Name="Weapon2", Damage=10, CharacterId=2 }
                );

            modelBuilder.Entity<CharacterSkill>().HasData(
                new CharacterSkill { CharacterId=1, SkillId=2},
                new CharacterSkill { CharacterId=2, SkillId=1},
                new CharacterSkill { CharacterId=2, SkillId=3}
                );
        }
    }
}
