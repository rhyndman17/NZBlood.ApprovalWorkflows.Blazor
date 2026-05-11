namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed class PpvLineItem
{
    public string ItemNumber { get; init; } = string.Empty;
    public string ItemDescription { get; init; } = string.Empty;
    public string VendorItemNumber { get; init; } = string.Empty;
    public decimal Variance { get; init; }
    public decimal PoCost { get; init; }
    public decimal InvoiceCost { get; init; }
    public string PoNumber { get; init; } = string.Empty;
    public string ShipmentNumber { get; init; } = string.Empty;
}
