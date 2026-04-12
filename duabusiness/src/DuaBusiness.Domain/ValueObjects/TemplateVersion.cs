namespace DuaBusiness.Domain.ValueObjects;

public sealed record TemplateVersion(int Major, int Minor, int Patch)
{
    public override string ToString() => $"{Major}.{Minor}.{Patch}";
}
