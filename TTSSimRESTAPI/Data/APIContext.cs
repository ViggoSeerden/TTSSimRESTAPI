using Microsoft.EntityFrameworkCore;
using TTSSimRESTAPI.Models;

namespace TTSSimRESTAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Item> Items { get; set; }

        public APIContext(DbContextOptions<APIContext> options ) :base(options)
        {

        }
    }
}