using Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace PruebaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Modelos

        public DbSet<Product> product { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<User> user { get; set; }
    }
}
