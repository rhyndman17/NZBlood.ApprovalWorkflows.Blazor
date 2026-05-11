using NZBlood.ApprovalWorkflows.Blazor.Models;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

public sealed class MockPpvApprovalService : IPpvApprovalService
{
    private static readonly List<PpvDashboardItem> DashboardItems =
    [
        new()
        {
            ReceiptNumber = "RCT100842",
            VendorId = "FISHER001",
            VendorName = "Fisher Scientific NZ Limited",
            VendorDocNumber = "INV-88421",
            SubmittedBy = "MasonT",
            SubmittedByEmailAddress = "mason.t@example.local",
            SubmittedDateTime = DateTime.Today.AddDays(-2).AddHours(10).AddMinutes(15),
            Approvers = "HyndmanR; ManagerReview",
            PurchaseOrders = "PO478193, PO478201",
            ManagerApproval = 1,
            ManagerApprovalDisplay = "Required",
            Comment = "Invoice pricing exceeds PO by more than threshold due to freight surcharge.",
            FirstLevelApprover = "HyndmanR"
        },
        new()
        {
            ReceiptNumber = "RCT100857",
            VendorId = "TERUMO002",
            VendorName = "Terumo Blood and Cell Technologies",
            VendorDocNumber = "NZ-104982",
            SubmittedBy = "KauriS",
            SubmittedByEmailAddress = "kauri.s@example.local",
            SubmittedDateTime = DateTime.Today.AddDays(-1).AddHours(14).AddMinutes(35),
            Approvers = "HyndmanR",
            PurchaseOrders = "PO478244",
            ManagerApproval = 0,
            ManagerApprovalDisplay = string.Empty,
            Comment = "Minor unit-cost variance for consumables.",
            FirstLevelApprover = "HyndmanR"
        },
        new()
        {
            ReceiptNumber = "RCT100866",
            VendorId = "BIOREP003",
            VendorName = "BioRep Services",
            VendorDocNumber = "BR-7731",
            SubmittedBy = "NgataA",
            SubmittedByEmailAddress = "ngata.a@example.local",
            SubmittedDateTime = DateTime.Today.AddHours(8).AddMinutes(50),
            Approvers = "HyndmanR",
            PurchaseOrders = "PO478301",
            ManagerApproval = 0,
            ManagerApprovalDisplay = string.Empty,
            Comment = "Supplier applied contract rate effective from April.",
            FirstLevelApprover = "HyndmanR"
        }
    ];

    public Task<IReadOnlyList<PpvDashboardItem>> GetDashboardAsync(UserContext user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<PpvDashboardItem>>(DashboardItems);
    }

    public Task<PpvApprovalDetail?> GetApprovalDetailAsync(string receiptNumber, UserContext user, CancellationToken cancellationToken = default)
    {
        var header = DashboardItems.FirstOrDefault(item =>
            string.Equals(item.ReceiptNumber, receiptNumber, StringComparison.OrdinalIgnoreCase));

        if (header is null)
        {
            return Task.FromResult<PpvApprovalDetail?>(null);
        }

        var detail = new PpvApprovalDetail
        {
            Header = header,
            TransactionComment = header.Comment + Environment.NewLine + "Mock data mode: no SQL Server connection is required.",
            Lines =
            [
                new()
                {
                    ItemNumber = "KIT-PLASMA-001",
                    ItemDescription = "Plasma collection kit with sterile tubing",
                    VendorItemNumber = "FS-7782",
                    PoNumber = header.PurchaseOrders.Split(',')[0].Trim(),
                    ShipmentNumber = "SHP-44091",
                    Variance = 428.52m,
                    PoCost = 11850.00m,
                    InvoiceCost = 12278.52m
                },
                new()
                {
                    ItemNumber = "FILTER-LEUKO-12",
                    ItemDescription = "Leukocyte reduction filter pack",
                    VendorItemNumber = "LF-1200",
                    PoNumber = header.PurchaseOrders.Split(',')[0].Trim(),
                    ShipmentNumber = "SHP-44091",
                    Variance = -76.18m,
                    PoCost = 3312.00m,
                    InvoiceCost = 3235.82m
                },
                new()
                {
                    ItemNumber = "FREIGHT",
                    ItemDescription = "Temperature-controlled freight surcharge",
                    VendorItemNumber = "FRT-COLD",
                    PoNumber = header.PurchaseOrders.Split(',')[0].Trim(),
                    ShipmentNumber = "SHP-44091",
                    Variance = 195.00m,
                    PoCost = 0m,
                    InvoiceCost = 195.00m
                }
            ],
            Attachments =
            [
                new() { FileName = header.VendorDocNumber + ".pdf", AttachmentId = header.ReceiptNumber + "-invoice" },
                new() { FileName = "approval-email.msg", AttachmentId = header.ReceiptNumber + "-email" },
                new() { FileName = "variance-analysis.xlsx", AttachmentId = header.ReceiptNumber + "-variance" }
            ],
            History =
            [
                new()
                {
                    StatusDateTime = header.SubmittedDateTime,
                    Status = "Submitted",
                    UserId = header.SubmittedBy,
                    Comment = "Submitted for approval from Dynamics GP."
                },
                new()
                {
                    StatusDateTime = header.SubmittedDateTime?.AddHours(2),
                    Status = "Reviewed",
                    UserId = header.FirstLevelApprover,
                    Comment = "Pricing variance checked against purchase order and supplier notes."
                }
            ]
        };

        return Task.FromResult<PpvApprovalDetail?>(detail);
    }

    public Task<bool> IsAdminUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }

    public Task<string> GetManagerEmailAddressAsync(string managerId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult("manager@example.local");
    }

    public Task<ApprovalActionResult> ApproveAsync(string receiptNumber, string comments, UserContext user, int requiresManagerApproval, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(string.IsNullOrWhiteSpace(comments)
            ? new ApprovalActionResult(false, "Comments are required to process an approval.")
            : new ApprovalActionResult(true, "Mock approval completed. No database update was made."));
    }

    public Task<ApprovalActionResult> RejectAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(string.IsNullOrWhiteSpace(comments)
            ? new ApprovalActionResult(false, "Comments are required to process a rejection.")
            : new ApprovalActionResult(true, "Mock rejection completed. No database update was made."));
    }

    public Task<ApprovalActionResult> ResetAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ApprovalActionResult(true, "Mock reset completed. No database update was made."));
    }

    public Task<AttachmentFile?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        var content = System.Text.Encoding.UTF8.GetBytes("Mock attachment content for " + attachmentId);
        return Task.FromResult<AttachmentFile?>(new AttachmentFile(attachmentId + ".txt", content, "text/plain"));
    }

    public Task<AttachmentFile?> GetPurchaseOrderPdfAsync(string receiptNumber, string poNumber, CancellationToken cancellationToken = default)
    {
        var content = System.Text.Encoding.UTF8.GetBytes("Mock purchase order PDF content for " + receiptNumber + " / " + poNumber);
        return Task.FromResult<AttachmentFile?>(new AttachmentFile(poNumber + ".txt", content, "text/plain"));
    }
}
