using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configuration;
internal sealed class TagConfiguration : IEntityTypeConfiguration<Core.Tag.Tag>
{
    /// <summary>
    ///     Configures the entity of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Core.Tag.Tag> builder)
    {
        builder.ToTable("Tag");

        builder
            .HasKey(x => x.TagId);

        builder
            .Property(x => x.TagId)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Description)
            .HasMaxLength(150);

        builder.HasQueryFilter(x => x.DeletedDate == null);
    }
}
