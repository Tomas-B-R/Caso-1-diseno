using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Interfaces;

public interface IExtractionStrategy
{
    string Name { get; }

    bool CanHandle(DocumentType documentType);
}
