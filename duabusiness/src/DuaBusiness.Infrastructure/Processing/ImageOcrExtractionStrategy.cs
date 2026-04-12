using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class ImageOcrExtractionStrategy : IExtractionStrategy
{
    public string Name => "ImageOcr";

    public bool CanHandle(DocumentType documentType) => documentType == DocumentType.Image;
}
