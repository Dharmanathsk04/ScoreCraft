using FluentValidation;
using Modules.Quiz.Core.QuestionAggregate;

namespace Modules.Quiz.Application.Question.QuestionSet.Create;
public sealed class CreateQuestionSetCommandValidator : AbstractValidator<CreateQuestionSetCommand>
{
    public CreateQuestionSetCommandValidator(IQuestionSetRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters")
            .MustAsync(async (name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower());
                return !isNameAlreadyExists;
            })
            .WithMessage("Question Set name already exists");

        RuleFor(x => x.Details)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Details))
            .WithMessage("Description can not be less than 10 characters and can not be more than 150 characters");

    }
}
