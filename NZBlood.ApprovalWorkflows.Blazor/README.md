# NZBlood Approval Workflows - Blazor Migration

This project is a side-by-side Blazor Server replacement for the existing Wisej approval workflow. It is currently focused on the active PPV approval process only.

The deprecated GLI approval flow has intentionally not been ported.

## Current State

The Blazor project is functional in mock-data mode and builds cleanly.

Verified build command:

```powershell
dotnet build .\NZBlood.ApprovalWorkflows.Blazor.csproj
```

Last known result:

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

## Migration Scope

Migrated so far:

- PPV approvals dashboard with card-based approval queue.
- Dashboard side-panel review and approval workflow.
- Receipt review page.
- Purchase order line item review on both dashboard panel and full review page.
- Invoice attachment list and download endpoints.
- Purchase order download endpoint.
- Approve, reject, and admin reset service paths.
- Admin and manager visibility rules.
- Mock data mode for UI review without GP SQL objects.
- SQL-backed service layer for later deployment near the real database.
- Syncfusion grid, dialog, toast, and theme integration.
- Header logo support through `wwwroot/brand_logo.png`.
- Production wiring checklist for SQL/deployment discovery.

Not migrated:

- GLI approval workflow.
- Wisej-specific PDF preview form.
- Production authentication configuration.
- Production database connection/config deployment.

## Technology Choices

- Framework: Blazor Server on `net8.0-windows`.
- UI library: Syncfusion Blazor Community License components.
- Data access: `System.Data.SqlClient` with parameterized SQL and stored procedure calls.
- Identity: current implementation mirrors the Wisej approach by resolving the Windows/domain user through `HttpContext.User.Identity.Name` with an environment username fallback.
- AD lookup: `System.DirectoryServices.DirectorySearcher`, which is why the project targets `net8.0-windows`.

## Important Files

- `Program.cs`  
  Registers Razor components, Syncfusion, user context, PPV service, and attachment endpoints.

- `appsettings.json`  
  Holds deployment configuration. `ApprovalWorkflow:UseMockData` is `false` for SQL-backed IIS deployment, with `appsettings.Development.json` overriding local development back to mock mode.

- `Components/Pages/Dashboard.razor`  
  PPV approval workbench with receipt cards, search, sort, KPI strip, side-panel receipt review, attachments, purchase order lines, approval comments, approve/reject/admin reset actions, Syncfusion confirmation dialog, and toast feedback.

- `Components/Pages/PpvReview.razor`  
  Full-page receipt review fallback/deep-link route with receipt details, approval comments, attachments, purchase order line items, status history, Syncfusion dialog confirmations, and toast feedback.

- `Services/PpvApprovalService.cs`  
  SQL-backed implementation for the real workflow.

- `Services/MockPpvApprovalService.cs`  
  Mock implementation for UI review without database access.

- `Services/UserContextService.cs`  
  Resolves current user, email, admin flag, and manager state.

- `wwwroot/app.css`  
  Office 365-inspired visual styling layered over Syncfusion Bootstrap 5 theme, including dashboard cards, side-panel review layout, logo styling, and responsive behavior.

- `wwwroot/brand_logo.png`  
  Optional header logo shown on the dashboard and full review page. If the logo cannot load, the UI falls back to the text mark.

- `Production-Wiring-Checklist.md`  
  Checklist for collecting SQL object definitions, sample result sets, stored procedure behavior, identity details, attachment handling, deployment details, and end-to-end workflow examples before production wiring.

- `IIS-Deployment-DevServer.md`  
  IIS deployment notes for the `NZBLOOD` dev server, including publish output, app pool settings, authentication, appsettings values, SQL permissions, and smoke-test steps.

## Data Mode

The base appsettings file is wired for the production-like development server:

```json
"ApprovalWorkflow": {
  "UseMockData": false,
  "TargetDevServer": "NZBLOOD",
  "RequireHttpsRedirection": false
}
```

The checked-in `appsettings.Development.json` keeps local `Development` runs in mock mode.

When `UseMockData` is `true`, `Program.cs` registers `MockPpvApprovalService`.

When `UseMockData` is `false`, `Program.cs` registers `PpvApprovalService`, which expects the GP/workflow SQL objects to exist.

Mock mode currently makes the user a normal approver, so the dashboard side panel and full review page show:

- Approve
- Reject

The Reset action is shown only for admin users. In real SQL mode, admin status comes from `nzbApprovalAdmins`. In mock mode, it is controlled by `MockPpvApprovalService.IsAdminUserAsync`.

In the dashboard, successful approve/reject/reset actions remove the processed receipt from the visible card queue.

## SQL Objects Expected In Real Mode

The SQL-backed implementation expects these objects to exist in the configured database:

- `nzbWFPPVDashboard`
- `nzbWFPPVApproverList`
- `nzbWFPPVTransactionLne`
- `nzbWFPPVTransactionHdr`
- `nzbWFPPVInvoiceHistory`
- `nzbWFPPVInvoiceStatus`
- `nzbPPVSetup`
- `nzbApprovalAdmins`
- `CO00101`
- `CO00102`
- `coAttachmentItems`

Stored procedures:

- `nzbWFPPVReceiptApproval`
- `nzbWFPPVReceiptReject`
- `nzbWFPPVReceiptReset`
- `nzbWFPPVCreateReceiptManagerApproval`

Stored procedure parameter ordering has been checked against the supplied SQL object scripts. The existing stored procedures do not show explicit transactions, so SQL-side all-or-nothing failure behaviour still needs integration confirmation. If a SQL-side change is required for the new UI, add wrapper/new SQL objects rather than changing the existing workflow objects in place.

## SQL Sample Export

Use `Scripts/Run-ProductionSampleExport.ps1` to collect JSON samples for Codex review and CSV mirrors for spreadsheet inspection:

```powershell
.\Scripts\Run-ProductionSampleExport.ps1 `
  -ConnectionString "<connection string>" `
  -ReceiptNumber "<known receipt number>" `
  -UserId "DOMAIN\username"
```

The runner writes to `ExportedSamples`, includes CSV files by default, and removes previous `.json`/`.csv` export files first unless `-KeepExistingOutput` is supplied. It can read the connection string from `$env:NZBLOOD_APPROVALWORKFLOW_CONNECTIONSTRING`. If `-ReceiptNumber` or `-UserId` is omitted, the export is not filtered by that value. Large binary fields are summarized with byte counts and first-8000-byte SHA-256 hashes.

## Syncfusion

Packages currently used:

- `Syncfusion.Blazor.Grid`
- `Syncfusion.Blazor.Popups`
- `Syncfusion.Blazor.Notifications`
- `Syncfusion.Blazor.Themes`

The Syncfusion license key should not be committed. It is configured through .NET user secrets:

```powershell
dotnet user-secrets set "Syncfusion:LicenseKey" "<license key>"
```

`Program.cs` reads `Syncfusion:LicenseKey` and registers it if present.

## UI Direction

The design direction is an Office 365-like internal approval workbench:

- Light neutral page background.
- White command surfaces and panels.
- Microsoft-style blue accent.
- Soft grey borders.
- Receipt cards for the primary approver queue.
- Simple dashboard search and sort rather than a dense grid-first workflow.
- Side-panel review surface that appears only after a receipt card is selected.
- Side panel acts as the primary approval page for normal approver workflow.
- Full review page remains available as a fallback/deep-link route.
- Wide desktop layout using up to `1680px`.
- Dashboard starts as a full-width card grid when no receipt is selected.
- When selected, the dashboard uses a narrower card queue plus a larger review panel.
- Side panel presents receipt data and attachments at the top, purchase order lines next, then approval notes/actions.
- Header logo support uses `wwwroot/brand_logo.png`, with a text fallback if missing.

Avoid drifting back toward:

- Dark dashboard styling.
- Marketing/landing-page visual patterns.
- Decorative cards that do not carry workflow information.
- One-hue palettes.
- Dense nested card-in-card structures.
- Reintroducing a grid as the primary dashboard experience unless approval volumes change significantly.

## Local Setup

Install the .NET 8 SDK.

Run from this project folder:

```powershell
dotnet restore
dotnet run --urls http://localhost:5240
```

For real SQL mode, set the connection string in `appsettings.json`, user secrets, environment configuration, or a local development settings file:

```powershell
dotnet user-secrets set "ConnectionStrings:ApprovalWorkflow" "<connection string>"
```

Avoid committing live credentials.

For IIS deployment on `NZBLOOD`, install the .NET 8 Hosting Bundle/runtime if it is not already present. HTTP-only hosting is currently expected, and HTTPS redirection is controlled by `ApprovalWorkflow:RequireHttpsRedirection`.

## Codex Handoff Notes

For future work, start here:

1. Build the project before making changes:

   ```powershell
   dotnet build .\NZBlood.ApprovalWorkflows.Blazor.csproj
   ```

2. If the app is running, the normal build output may be locked. Either stop the running app or verify with a separate output folder:

   ```powershell
   dotnet build .\NZBlood.ApprovalWorkflows.Blazor.csproj -o .\.verify-build
   ```

3. Keep mock mode available until the app is deployed near a database with the GP/workflow objects.

4. Do not reintroduce GLI unless explicitly requested.

5. Preserve the SQL-backed service contract while improving the UI. The mock and SQL services should continue to implement the same `IPpvApprovalService`.

6. Keep Syncfusion use selective: grids, dialogs, toast, and possibly future PDF viewer. Do not replace every control just because a Syncfusion component exists.

7. The dashboard side panel is now the preferred review/approval flow. Keep the full review page working, but treat it as a fallback and deep-link page unless the product direction changes.

8. If replacing the logo, keep the file at `wwwroot/brand_logo.png` or update the header references in both dashboard and full review page.

## Likely Next Steps

- Use `Production-Wiring-Checklist.md` to gather SQL structures, stored procedure behavior, identity details, attachment details, and deployment information.
- Validate the SQL service against a real GP/workflow database on the dev server.
- Add Windows Authentication configuration for hosted deployment.
- Add a mock role switch for approver/admin/manager UI review.
- Add inline PDF preview for attachments or POs if desired.
- Add tests around approval/reject/reset service behavior once database access can be abstracted or integration-tested.
