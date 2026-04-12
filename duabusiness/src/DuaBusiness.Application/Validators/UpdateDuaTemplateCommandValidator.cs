using DuaBusiness.Application.Contracts.Templates;

namespace DuaBusiness.Application.Validators;

public sealed class UpdateDuaTemplateCommandValidator
{
    public IReadOnlyCollection<string> Validate(UpdateDuaTemplateCommand command)
    {
        var errors = new List<string>();

        if (command.TemplateId == Guid.Empty)
        {
            errors.Add("A template identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            errors.Add("Template name is required.");
        }

        if (command.RequiredFields.Count == 0)
        {
            errors.Add("At least one required field must be registered.");
        }

        return errors;
    }
}
