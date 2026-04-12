using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Validators;

public sealed class GenerateDuaDocumentCommandValidator
{
    public IReadOnlyCollection<string> Validate(GenerateDuaDocumentCommand command)
    {
        var errors = new List<string>();

        if (command.JobId == Guid.Empty)
        {
            errors.Add("A processing job identifier is required.");
        }

        if (command.TemplateId == Guid.Empty)
        {
            errors.Add("A template identifier is required.");
        }

        return errors;
    }
}
