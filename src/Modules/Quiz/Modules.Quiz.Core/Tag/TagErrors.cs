using Shared.Core;

namespace Modules.Quiz.Core.Tag;

public struct TagErrors
{
    public static Error TagNameCanNotBeEmptyValidation => Error.Validation("Tag.Name", "Tag Name can not be empty");
    public static Error TagDescriptionValidation => Error.Validation("Tag.Description", "Tag Description can not be more than 150 characters");
    public static Error TagNotFound => Error.NotFound("Tag.TagNotFound", "Tag not found.");
}
