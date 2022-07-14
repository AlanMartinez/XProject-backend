using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XProject.Core.Entities;
using XProject.Core.Enumerations;

namespace XProject.Infrastructure.Data.Configuration
{
    internal class SecurityConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Security");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Role).HasConversion(x => x.ToString(), x => (RoleType)Enum.Parse(typeof(RoleType), x));

            builder.HasOne(x => x.User)
                .WithOne(u => u.Security)
                .HasForeignKey<User>(x => x.SecurityId);

        }
    }
}