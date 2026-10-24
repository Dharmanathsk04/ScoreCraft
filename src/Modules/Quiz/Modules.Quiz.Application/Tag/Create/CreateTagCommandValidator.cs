using FluentValidation;
using Modules.Quiz.Core.Tag;

namespace Modules.Quiz.Application.Tag.Create;
public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator(ITagRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .MustAsync(async (name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower());
                return !isNameAlreadyExists;
            })
            .WithMessage("Tag name already exists");

        RuleFor(x => x.Description)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description can not be less than 10 characters and can not be more than 150 characters");

    }
}
