using CQRS_JWTApp.API.Core.Domain;
using CQRS_JWTApp.API.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CQRS_JWTApp.API.Persistance.Context
{
    public class CqrsJwtContext : DbContext
    {
        public CqrsJwtContext(DbContextOptions<CqrsJwtContext> options) : base(options)
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
