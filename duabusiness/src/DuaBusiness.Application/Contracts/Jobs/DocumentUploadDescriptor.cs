using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record DocumentUploadDescriptor(
    string FileName,
    string ContentType,
    long SizeInBytes,
    DocumentType DocumentType);
