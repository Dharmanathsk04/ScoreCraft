using FluentValidation;
using Modules.Quiz.Core.QuestionAggregate;

namespace Modules.Quiz.Application.Question.Question.Update;
internal sealed class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator(IQuestionSetRepository repository)
    {
        RuleFor(x => x.QuestionId)
            .NotNull()
            .WithMessage("Question Id can not null")
            .GreaterThan(0)
            .WithMessage("Question Id value can not be 0 or less than 0");

        RuleFor(x => x.Question)
            .NotEmpty()
            .WithMessage("Question can not be empty")
            .Length(5, 200)
            .WithMessage("Question can not be less than 5 characters and can not be more than 200 characters");

        RuleFor(x => x.Details)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Details))
            .WithMessage("Details can not be less than 10 characters and can not be more than 150 characters");
    }
}
