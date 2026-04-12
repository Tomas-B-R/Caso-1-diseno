using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class ExcelExtractionStrategy : IExtractionStrategy
{
    public string Name => "ExcelParsing";

    public bool CanHandle(DocumentType documentType) => documentType == DocumentType.Excel;
}
