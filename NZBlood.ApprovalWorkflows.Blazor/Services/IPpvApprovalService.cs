using NZBlood.ApprovalWorkflows.Blazor.Models;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

public interface IPpvApprovalService
{
    Task<IReadOnlyList<PpvDashboardItem>> GetDashboardAsync(UserContext user, CancellationToken cancellationToken = default);
    Task<PpvApprovalDetail?> GetApprovalDetailAsync(string receiptNumber, UserContext user, CancellationToken cancellationToken = default);
    Task<bool> IsAdminUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<string> GetManagerEmailAddressAsync(string managerId, CancellationToken cancellationToken = default);
    Task<ApprovalActionResult> ApproveAsync(string receiptNumber, string comments, UserContext user, int requiresManagerApproval, CancellationToken cancellationToken = default);
    Task<ApprovalActionResult> RejectAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default);
    Task<ApprovalActionResult> ResetAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default);
    Task<AttachmentFile?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken = default);
    Task<AttachmentFile?> GetPurchaseOrderPdfAsync(string receiptNumber, string poNumber, CancellationToken cancellationToken = default);
}
