using System.DirectoryServices;
using NZBlood.ApprovalWorkflows.Blazor.Models;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

public sealed class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserContextService> _logger;
    private readonly IPpvApprovalService _approvalService;

    public UserContextService(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        ILogger<UserContextService> logger,
        IPpvApprovalService approvalService)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _logger = logger;
        _approvalService = approvalService;
    }

    public async Task<UserContext> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var domainName = _configuration["ApprovalWorkflow:DomainName"] ?? string.Empty;
        var identityName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        var userId = string.IsNullOrWhiteSpace(identityName)
            ? Environment.UserName
            : identityName;

        userId = userId.Replace(domainName + @"\", string.Empty, StringComparison.OrdinalIgnoreCase);

        var emailAddress = GetUserEmail(userId);
        var isAdmin = await _approvalService.IsAdminUserAsync(userId, cancellationToken);
        var managerEmail = await _approvalService.GetManagerEmailAddressAsync(userId, cancellationToken);

        return new UserContext
        {
            UserId = userId,
            EmailAddress = emailAddress,
            IsAdminUser = isAdmin,
            IsManager = !string.IsNullOrWhiteSpace(managerEmail),
            ManagerEmailAddress = managerEmail
        };
    }

    private string GetUserEmail(string userId)
    {
        if (string.Equals(userId, "rhadmin", StringComparison.OrdinalIgnoreCase))
        {
            userId = "HyndmanR";
        }

        try
        {
            var accountName = userId.Split('\\').Last().ToLowerInvariant();
            using var searcher = userId.Contains('\\')
                ? new DirectorySearcher("LDAP://" + userId.Split('\\').First().ToLowerInvariant())
                : new DirectorySearcher();

            searcher.Filter = "(&(ObjectClass=person)(sAMAccountName=" + accountName + "))";

            var result = searcher.FindOne();
            return result?.Properties["mail"]?.Count > 0
                ? Convert.ToString(result.Properties["mail"][0]) ?? string.Empty
                : string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to resolve email address for {UserId}.", userId);
            return string.Empty;
        }
    }
}
