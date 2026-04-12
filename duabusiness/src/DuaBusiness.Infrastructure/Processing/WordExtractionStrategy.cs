using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class WordExtractionStrategy : IExtractionStrategy
{
    public string Name => "WordParsing";

    public bool CanHandle(DocumentType documentType) => documentType == DocumentType.Word;
}
