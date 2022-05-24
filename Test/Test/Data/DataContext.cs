using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> User { get; set; }
    }
}
