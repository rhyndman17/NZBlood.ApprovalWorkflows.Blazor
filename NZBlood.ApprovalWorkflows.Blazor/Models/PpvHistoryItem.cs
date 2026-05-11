namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed class PpvHistoryItem
{
    public DateTime? StatusDateTime { get; init; }
    public string UserId { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
}
