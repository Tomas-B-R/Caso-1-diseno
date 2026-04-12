namespace DuaBusiness.Infrastructure.Configuration;

public sealed class KeyVaultOptions
{
    public const string SectionName = "KeyVault";

    public string VaultUri { get; init; } = string.Empty;
}
