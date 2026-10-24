using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Quiz.Core.QuestionAggregate;

namespace Modules.Quiz.Infrastructure.Data.Configuration;
internal sealed class QuestionSetConfiguration : IEntityTypeConfiguration<QuestionSet>
{
    public void Configure(EntityTypeBuilder<QuestionSet> builder)
    {
        builder
            .Property(x => x.QuestionSetId)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Details)
            .HasMaxLength(200);

        builder.Property(x => x.SetCode)
            .HasMaxLength(10);

        builder.HasMany(x => x.Questions)
            .WithOne(x => x.QuestionSet)
            .HasForeignKey(x => x.QuestionSetId);

        builder.HasQueryFilter(x => x.DeletedDate == null);
    }
}
