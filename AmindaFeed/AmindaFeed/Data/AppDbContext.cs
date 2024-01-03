using Microsoft.EntityFrameworkCore;

namespace AmindaFeed.Data
{
    public class AppDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresSQL"));
        }

        public DbSet<AmindaProductDb> AmindaProductDb { get; set; }
    }
}
