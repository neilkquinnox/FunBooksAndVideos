using FunBooksAndVideos.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FunBooksAndVideos.WebApi.Infrastructure.Database
{
    public interface IDatabaseContext
    {
        DbSet<ProductEntity> Products { get; set; }
        DbSet<CustomerEntity> Customers { get; set; }
        DbSet<MembershipEntity> Memberships { get; set; }
        DbSet<CustomerMembershipEntity> CustomerMemberships { get; set; }
        DbSet<OrderEntity> Orders { get; set; }
        DbSet<OrderProductEntity> OrderProducts { get; set; }
        Task<int> SaveChangesAsync();
    }
}