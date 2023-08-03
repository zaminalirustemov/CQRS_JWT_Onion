using JwtApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtApp.Persistance.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUsers).HasForeignKey(x => x.AppRoleId);
        }
    }
}
