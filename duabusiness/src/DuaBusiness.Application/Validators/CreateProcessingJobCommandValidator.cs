using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Validators;

public sealed class CreateProcessingJobCommandValidator
{
    public IReadOnlyCollection<string> Validate(CreateProcessingJobCommand command)
    {
        var errors = new List<string>();

        if (command.TemplateId == Guid.Empty)
        {
            errors.Add("A template identifier is required.");
        }

        if (command.Documents.Count == 0)
        {
            errors.Add("At least one input document is required.");
        }

        return errors;
    }
}
