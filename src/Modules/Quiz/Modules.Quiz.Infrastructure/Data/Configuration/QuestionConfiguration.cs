using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configuration;
internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Core.QuestionAggregate.Question>
{
    public void Configure(EntityTypeBuilder<Core.QuestionAggregate.Question> builder)
    {
        builder.Property(x => x.QuestionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AskedQuestion)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Discussion)
            .HasMaxLength(600);
    }
}
