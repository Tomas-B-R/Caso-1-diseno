using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class PdfExtractionStrategy : IExtractionStrategy
{
    public string Name => "PdfExtraction";

    public bool CanHandle(DocumentType documentType) => documentType == DocumentType.Pdf;
}
