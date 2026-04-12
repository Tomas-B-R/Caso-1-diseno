namespace DuaBusiness.Domain.Events;

public sealed record DuaTemplatePublishedDomainEvent(Guid TemplateId, string Version, DateTimeOffset PublishedAtUtc);
