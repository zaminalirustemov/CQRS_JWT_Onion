using JwtApp.Domain.Entities;
using JwtApp.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Persistance.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {
        }
        public DbSet<AppRole> AppRoles => this.Set<AppRole>();
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<Category> Categories => this.Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
