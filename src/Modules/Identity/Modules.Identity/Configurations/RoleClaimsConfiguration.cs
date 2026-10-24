using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Modules.Identity.Configurations;
internal sealed class RoleClaimsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder
            .ToTable("RoleClaims")
            .HasKey(x => x.Id);
        builder
            .HasOne<IdentityRole<Guid>>()
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}