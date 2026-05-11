using NZBlood.ApprovalWorkflows.Blazor.Models;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

public interface IUserContextService
{
    Task<UserContext> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
