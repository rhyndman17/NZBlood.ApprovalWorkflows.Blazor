namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed class UserContext
{
    public string UserId { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
    public bool IsAdminUser { get; init; }
    public bool IsManager { get; init; }
    public string ManagerEmailAddress { get; init; } = string.Empty;
}
