using CQRS_JWTApp.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS_JWTApp.MVC.Configurations
{
    public class UserLoginModelConfiguration : IEntityTypeConfiguration<UserLoginModel>
    {
        public void Configure(EntityTypeBuilder<UserLoginModel> builder)
        {
            builder.Property(x => x.Username).IsRequired(true);
            builder.Property(x => x.Username).HasMaxLength(200);

            builder.Property(x => x.Password).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(200);
        }
    }
}
