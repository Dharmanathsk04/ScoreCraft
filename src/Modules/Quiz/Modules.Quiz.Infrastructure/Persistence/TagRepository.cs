using Modules.Quiz.Core.Tag;
using Modules.Quiz.Infrastructure.Data;

namespace Modules.Quiz.Infrastructure.Persistence;

internal sealed class TagRepository(QuestionModuleDbContext context)
    : BaseRepository<Tag>(context), ITagRepository;
