namespace PollStar.Core.Configuration;

public class AzureStorageConfiguration
{
    public const string SectionName = "Azure";
    public string StorageAccount { get; set; } = default!;
    public string StorageKey { get; set; } = default!;
}