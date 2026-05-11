namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed class PpvDashboardItem
{
    public string ReceiptNumber { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string VendorName { get; init; } = string.Empty;
    public string VendorDocNumber { get; init; } = string.Empty;
    public string SubmittedBy { get; init; } = string.Empty;
    public string SubmittedByEmailAddress { get; init; } = string.Empty;
    public DateTime? SubmittedDateTime { get; init; }
    public string Approvers { get; init; } = string.Empty;
    public string PurchaseOrders { get; init; } = string.Empty;
    public int ManagerApproval { get; init; }
    public string ManagerApprovalDisplay { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
    public string FirstLevelApprover { get; init; } = string.Empty;
}
