using Microsoft.EntityFrameworkCore;
using Fluentify.Models.Database.Tables;
namespace Fluentify.Database
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public StoreDbContext() { }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer("Server=tcp:myproject333.database.windows.net,1433;Initial Catalog=myproject;Persist Security Info=False;User ID=admin1;Password=Orewit123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
            .Property(u => u.RegDate)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");
        }

    }
}