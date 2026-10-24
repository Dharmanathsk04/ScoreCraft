using FluentValidation;
using Modules.Quiz.Core.QuestionAggregate;

namespace Modules.Quiz.Application.Question.QuestionSet.Update;
internal sealed class UpdateQuestionSetCommandValidator : AbstractValidator<UpdateQuestionSetCommand>
{
    public UpdateQuestionSetCommandValidator(IQuestionSetRepository repository)
    {
        RuleFor(x => x.QuestionSetId)
            .NotNull()
            .WithMessage("Question Set Id can not null")
            .GreaterThan(0)
            .WithMessage("Question Set Id value can not be 0 or less than 0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters")
            .MustAsync(async (command, name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower() && x.QuestionSetId != command.QuestionSetId);
                return !isNameAlreadyExists;
            })
            .WithMessage("Question Set name is already exists");

        RuleFor(x => x.Details)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Details))
            .WithMessage("Details can not be less than 10 characters and can not be more than 150 characters");
    }
}
