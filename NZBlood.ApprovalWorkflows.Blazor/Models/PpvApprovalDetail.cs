namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed class PpvApprovalDetail
{
    public required PpvDashboardItem Header { get; init; }
    public required IReadOnlyList<PpvLineItem> Lines { get; init; }
    public required IReadOnlyList<PpvAttachment> Attachments { get; init; }
    public required IReadOnlyList<PpvHistoryItem> History { get; init; }
    public string TransactionComment { get; init; } = string.Empty;
}
