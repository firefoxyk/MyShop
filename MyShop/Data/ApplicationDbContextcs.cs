using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<Catigory> Catigory { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
