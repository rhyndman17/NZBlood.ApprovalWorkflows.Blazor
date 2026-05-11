using NZBlood.ApprovalWorkflows.Blazor.Components;
using NZBlood.ApprovalWorkflows.Blazor.Services;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();
if (builder.Configuration.GetValue<bool>("ApprovalWorkflow:UseMockData"))
{
    builder.Services.AddScoped<IPpvApprovalService, MockPpvApprovalService>();
}
else
{
    builder.Services.AddScoped<IPpvApprovalService, PpvApprovalService>();
}

var app = builder.Build();

var syncfusionLicenseKey = builder.Configuration["Syncfusion:LicenseKey"];
if (!string.IsNullOrWhiteSpace(syncfusionLicenseKey))
{
    SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    if (builder.Configuration.GetValue<bool>("ApprovalWorkflow:RequireHttpsRedirection"))
    {
        app.UseHsts();
    }
}

if (builder.Configuration.GetValue<bool>("ApprovalWorkflow:RequireHttpsRedirection"))
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/attachments/ppv/{attachmentId}", async (
    string attachmentId,
    IPpvApprovalService approvalService,
    CancellationToken cancellationToken) =>
{
    var attachment = await approvalService.GetAttachmentAsync(attachmentId, cancellationToken);
    return attachment is null
        ? Results.NotFound()
        : Results.File(attachment.Content, attachment.ContentType, attachment.FileName);
});

app.MapGet("/attachments/ppv-po/{receiptNumber}/{poNumber}", async (
    string receiptNumber,
    string poNumber,
    IPpvApprovalService approvalService,
    CancellationToken cancellationToken) =>
{
    var attachment = await approvalService.GetPurchaseOrderPdfAsync(receiptNumber, poNumber, cancellationToken);
    return attachment is null
        ? Results.NotFound()
        : Results.File(attachment.Content, attachment.ContentType, attachment.FileName);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
