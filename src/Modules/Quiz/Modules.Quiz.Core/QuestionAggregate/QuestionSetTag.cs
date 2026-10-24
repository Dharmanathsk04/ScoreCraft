using TagCore = Modules.Quiz.Core.Tag.Tag;

namespace Modules.Quiz.Core.QuestionAggregate;
public sealed class QuestionSetTag
{
    public long QuestionSetId { get; private set; }
    public QuestionSet QuestionSet { get; private set; }

    public long TagId { get; private set; }
    public TagCore Tag { get; private set; }

    internal QuestionSetTag()
    {
        
    }

    internal QuestionSetTag(TagCore tag, QuestionSet questionSet)
    {
        QuestionSet = questionSet;
        Tag = tag;
    }
}
