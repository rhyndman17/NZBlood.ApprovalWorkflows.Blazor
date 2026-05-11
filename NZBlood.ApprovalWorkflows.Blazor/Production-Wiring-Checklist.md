# Production Wiring Checklist

Use this checklist to gather the SQL, identity, attachment, and deployment details needed to wire the Blazor approval workflow into the production-like development environment.

## 1. SQL Object Definitions

- [X] Provide `CREATE TABLE` scripts for required tables.
- [X] Provide `CREATE VIEW` scripts for required views.
- [X] Provide stored procedure definitions.
- [ ] Include any user-defined functions used by the views or procedures.
- [X] Include any lookup/setup tables used by the workflow.

Expected objects from the current service:

- [X] `nzbWFPPVDashboard`
- [X] `nzbWFPPVApproverList`
- [X] `nzbWFPPVTransactionLne`
- [X] `nzbWFPPVTransactionHdr`
- [X] `nzbWFPPVInvoiceHistory`
- [X] `nzbWFPPVInvoiceStatus`
- [X] `nzbPPVSetup`
- [X] `nzbApprovalAdmins`
- [X] `CO00101`
- [X] `CO00102`
- [X] `coAttachmentItems`
- [X] `nzbWFPPVReceiptApproval`
- [X] `nzbWFPPVReceiptReject`
- [X] `nzbWFPPVReceiptReset`
- [X] `nzbWFPPVCreateReceiptManagerApproval`

## 2. Example Result Sets

Provide small anonymised samples for each data shape the app reads.

Helper script:

```powershell
.\Scripts\Run-ProductionSampleExport.ps1 `
  -ConnectionString "<connection string>" `
  -ReceiptNumber "<known receipt number>" `
  -UserId "DOMAIN\username"
```

The runner calls `Scripts\Export-ProductionSampleData.ps1`, exports JSON plus CSV by default, and writes to `ExportedSamples`. It removes previous `.json`/`.csv` export files first unless `-KeepExistingOutput` is supplied. It can also read the connection string from `$env:NZBLOOD_APPROVALWORKFLOW_CONNECTIONSTRING`. If `-ReceiptNumber` or `-UserId` is omitted, the export is not filtered by that value. Binary attachment/PO fields are exported as byte counts and first-8000-byte SHA-256 hashes, not raw file contents.

- [X] Dashboard row.
- [X] Receipt/header detail row.
- [X] Purchase order line rows.
- [X] Invoice attachment metadata rows.
- [X] Status history rows.
- [X] Approver rows.
- [X] Admin rows.
- [X] Manager/setup rows.

## 3. Stored Procedure Behaviour

For each approval procedure, capture:

- [X] Procedure name.
- [X] Parameter list.
- [X] Parameter order.
- [X] Required user ID format.
- [X] Required comment/status values.
- [X] Success behaviour.
- [X] Failure behaviour.
- [X] Return rows, if any.
- [X] Output parameters, if any.
- [X] Return codes, if any.
- [X] Whether the app should expect SQL exceptions for business rule failures.

Procedures to document:

- [X] `nzbWFPPVReceiptApproval` - parameters are `@receiptNumber`, `@comment`, `@userId`, `@isManager`, `@isAdmin`. Creates/updates the APPR batch, updates GP receipt state, clears workflow rows, records history status `2`, and sends the batch for posting.
- [X] `nzbWFPPVReceiptReject` - parameters are `@receiptNumber`, `@comment`, `@userId`, `@isManager`, `@isAdmin`. Moves receipt to the `REJECT` batch, clears workflow rows/current approvers, and records history status `3`.
- [X] `nzbWFPPVReceiptReset` - parameters are `@receiptNumber`, `@comment`, `@userId`, `@isManager`, `@isAdmin`. Moves receipt to the `RESET` batch, clears current workflow rows/current approvers, and records history status `9` so it can be resubmitted.
- [X] `nzbWFPPVCreateReceiptManagerApproval` - parameters are `@receiptNumber`, `@managerAccountName`, `@managerEmailAddress`, `@comment`, `@userId`, `@isManager`, `@isAdmin`. Replaces lower-level approvers with the active PPV manager and records lower-level approval history status `2`.

Failure note: the app only removes records from the UI after a stored procedure call completes successfully. Stored procedure calls are wrapped in an app-managed SQL transaction with `XACT_ABORT` enabled, so SQL exceptions should roll back the procedure work. Business-rule failures that do not raise SQL exceptions still need integration confirmation. If a SQL-side change is required, create new wrapper SQL objects rather than altering the existing procedures.

## 4. Identity And Permissions

- [X] Confirm whether production uses Windows Authentication.
- [X] Confirm the exact SQL user identifier format.
- [X] Confirm whether SQL expects `DOMAIN\username`, `username`, email address, or another value.
- [X] Confirm how approvers are matched to dashboard rows.
- [X] Confirm how admin users are identified.
- [X] Confirm how manager users are identified.
- [ ] Confirm whether app pool identity needs SQL permissions.
- [X] Confirm whether end-user delegation/impersonation is required.

Identity notes: there is no application login. The app resolves the Windows/AD user, normalises it to the short AD account name, and compares it to `nzbWFPPVApproverList.AccountName`. If there is no match, the app opens normally and shows the empty approvals banner. Admins are identified from `nzbApprovalAdmins` where `Inactive='No'`. Managers are identified from active `nzbPPVSetup` rows for `ApprovalID='PPV1'`.

## 5. Attachment Handling

- [X] Confirm where invoice attachments are stored.
- [X] Confirm whether attachment content is stored as binary data in SQL.
- [X] Confirm whether attachment rows point to files outside SQL.
- [X] Confirm how attachment IDs map to downloadable files.
- [X] Confirm expected file names.
- [X] Confirm expected content types.
- [X] Confirm how purchase order PDFs are retrieved or generated.
- [X] Confirm whether the app server needs filesystem access for attachments.

Attachment notes: invoice attachments are stored in SQL as `coAttachmentItems.binaryblob` and are matched through `CO00102`/`CO00101` using `Attachment_ID` and `BusObjKey` containing the receipt number. Purchase order PDFs are read from `nzbWFPPVTransactionLne.POBinary`. Browser download location is controlled by the user's browser settings; the app does not need filesystem access for attachment storage.

## 6. Deployment Details

- [X] Confirm target dev server name.
- [X] Confirm whether hosting is IIS, Kestrel, or IIS reverse proxy to Kestrel.
- [ ] Confirm .NET 8 runtime availability on the server.
- [ ] Confirm Windows Authentication configuration.
- [X] Confirm HTTPS requirement.
- [ ] Confirm app pool identity.
- [X] Confirm connection string location.
- [ ] Confirm whether the app server can reach the GP SQL Server directly.
- [ ] Confirm firewall/network requirements.
- [X] Confirm Syncfusion license configuration method.
- [X] Confirm logging location and retention expectations.

Deployment notes: target dev server is `NZBLOOD`, hosted on IIS. .NET 8 Hosting Bundle/runtime is a prerequisite if it is not already installed. HTTP only is required at this stage; HTTPS redirection is disabled through `ApprovalWorkflow:RequireHttpsRedirection=false`. The SQL connection string is in `appsettings.json` and is SQL-user based, not trusted connection based. Syncfusion license registration is already part of `Program.cs`. Any persistent application logging should be written to SQL Server; no new logging SQL object has been added yet.

IIS deployment details are documented in `IIS-Deployment-DevServer.md`.

## 7. Real Workflow Scenario

Provide one anonymised end-to-end example.

- [X] User opens the approval dashboard.
- [X] User identity resolved by the app.
- [X] Dashboard shows receipt(s) for that user.
- [ ] User opens a receipt card.
- [ ] User reviews receipt data.
- [ ] User reviews attachments.
- [ ] User reviews PO line items.
- [ ] User enters comments.
- [ ] User approves or rejects.
- [X] Stored procedure called.
- [X] SQL records updated.
- [X] Receipt disappears from approver queue.
- [X] Manager approval path confirmed, if applicable.
- [ ] Final status history confirmed.

## 8. Local Wiring Notes

Use this section while adapting the local service implementation.

- [X] Confirm current `PpvApprovalService` SQL queries match real object names.
- [X] Confirm all selected column names match real view/table columns.
- [ ] Confirm all nullable fields are handled correctly.
- [ ] Confirm all date/time fields are handled correctly.
- [ ] Confirm all money/decimal fields are handled correctly.
- [X] Confirm attachment download endpoints work against real metadata/content.
- [X] Confirm approve/reject/reset actions return useful messages.
- [ ] Confirm mock mode still works after SQL changes.

SQL object change policy: preserve the existing backend objects while the current workflow remains live. If backend changes become necessary for the new UI, create new SQL objects or wrapper procedures rather than updating existing objects in place.
