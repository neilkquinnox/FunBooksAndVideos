using FunBooksAndVideos.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FunBooksAndVideos.WebApi.Infrastructure.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MembershipEntity> Memberships { get; set; }
        public DbSet<CustomerMembershipEntity> CustomerMemberships { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderProductEntity> OrderProducts { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.price)
                .HasColumnType("decimal(18,4)");
        }
    }
}