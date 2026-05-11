using System.Data;
using System.Data.SqlClient;
using NZBlood.ApprovalWorkflows.Blazor.Models;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

public sealed class PpvApprovalService : IPpvApprovalService
{
    private readonly string _connectionString;

    public PpvApprovalService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ApprovalWorkflow")
            ?? throw new InvalidOperationException("Connection string 'ApprovalWorkflow' is not configured.");
    }

    public async Task<IReadOnlyList<PpvDashboardItem>> GetDashboardAsync(UserContext user, CancellationToken cancellationToken = default)
    {
        var sql = """
            select ReceiptNumber,VendorID,VendorName,VendorDocNumber,SubmittedBy,SubmittedByEmailAddress,
                   SubmittedDateTime,Approvers,PurchaseOrders,ManagerApproval,ManagerApprovalDisplay,Comment,FirstLevelApprover
            from nzbWFPPVDashboard
            """;

        if (!user.IsAdminUser)
        {
            sql += " where ReceiptNumber in (select ReceiptNumber from nzbWFPPVApproverList where AccountName=@userId)";
        }

        sql += " order by ReceiptNumber";

        var items = new List<PpvDashboardItem>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", user.UserId);

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(ReadDashboardItem(reader));
        }

        return items;
    }

    public async Task<PpvApprovalDetail?> GetApprovalDetailAsync(string receiptNumber, UserContext user, CancellationToken cancellationToken = default)
    {
        var header = await GetDashboardItemAsync(receiptNumber, user, cancellationToken);
        if (header is null)
        {
            return null;
        }

        return new PpvApprovalDetail
        {
            Header = header,
            Lines = await GetLineItemsAsync(receiptNumber, cancellationToken),
            Attachments = await GetAttachmentsAsync(receiptNumber, cancellationToken),
            History = await GetHistoryAsync(receiptNumber, cancellationToken),
            TransactionComment = await GetPpvCommentAsync(receiptNumber, cancellationToken)
        };
    }

    public async Task<ApprovalActionResult> ApproveAsync(string receiptNumber, string comments, UserContext user, int requiresManagerApproval, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(comments))
        {
            return new ApprovalActionResult(false, "Comments are required to process an approval.");
        }

        var status = await GetUserApprovalStatusAsync(receiptNumber, user, cancellationToken);
        if (status != 1)
        {
            return new ApprovalActionResult(false, "Receipt has already been processed and cannot be approved.");
        }

        if (requiresManagerApproval == 1 && !user.IsManager)
        {
            var manager = await GetActivePpvManagerAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(manager.AccountName))
            {
                return new ApprovalActionResult(false, "A manager approval record is required, but no active PPV manager was found.");
            }

            await ExecuteStoredProcedureAsync(
                "nzbWFPPVCreateReceiptManagerApproval",
                cancellationToken,
                new SqlParameter("@receiptNumber", receiptNumber),
                new SqlParameter("@managerAccountName", manager.AccountName),
                new SqlParameter("@managerEmailAddress", manager.EmailAddress),
                new SqlParameter("@comments", comments),
                new SqlParameter("@userId", user.UserId),
                new SqlParameter("@isManager", Convert.ToInt32(user.IsManager)),
                new SqlParameter("@isAdminUser", Convert.ToInt32(user.IsAdminUser)));

            return new ApprovalActionResult(true, "Receipt has been sent for manager approval.");
        }

        await ExecuteStoredProcedureAsync(
            "nzbWFPPVReceiptApproval",
            cancellationToken,
            new SqlParameter("@receiptNumber", receiptNumber),
            new SqlParameter("@comments", comments),
            new SqlParameter("@userId", user.UserId),
            new SqlParameter("@isManager", Convert.ToInt32(user.IsManager)),
            new SqlParameter("@isAdminUser", Convert.ToInt32(user.IsAdminUser)));

        return new ApprovalActionResult(true, "Receipt has been approved.");
    }

    public async Task<ApprovalActionResult> RejectAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(comments))
        {
            return new ApprovalActionResult(false, "Comments are required to process a rejection.");
        }

        var status = await GetUserApprovalStatusAsync(receiptNumber, user, cancellationToken);
        if (status != 1)
        {
            return new ApprovalActionResult(false, "Receipt has already been processed and cannot be rejected.");
        }

        await ExecuteStoredProcedureAsync(
            "nzbWFPPVReceiptReject",
            cancellationToken,
            new SqlParameter("@receiptNumber", receiptNumber),
            new SqlParameter("@comments", comments),
            new SqlParameter("@userId", user.UserId),
            new SqlParameter("@isManager", Convert.ToInt32(user.IsManager)),
            new SqlParameter("@isAdminUser", Convert.ToInt32(user.IsAdminUser)));

        return new ApprovalActionResult(true, "Receipt has been rejected.");
    }

    public async Task<ApprovalActionResult> ResetAsync(string receiptNumber, string comments, UserContext user, CancellationToken cancellationToken = default)
    {
        var okToReset = await GetReceiptResetStatusAsync(receiptNumber, cancellationToken);
        if (okToReset != 1)
        {
            return new ApprovalActionResult(false, "Receipt has already been processed and cannot be reset.");
        }

        await ExecuteStoredProcedureAsync(
            "nzbWFPPVReceiptReset",
            cancellationToken,
            new SqlParameter("@receiptNumber", receiptNumber),
            new SqlParameter("@comments", comments),
            new SqlParameter("@userId", user.UserId),
            new SqlParameter("@isManager", Convert.ToInt32(user.IsManager)),
            new SqlParameter("@isAdminUser", Convert.ToInt32(user.IsAdminUser)));

        return new ApprovalActionResult(true, "Receipt has been reset.");
    }

    public async Task<AttachmentFile?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        const string sql = """
            select binaryblob, co2.filename
            from coAttachmentItems co1
            join CO00101 co2 on co1.Attachment_ID = co2.Attachment_ID
            where co1.attachment_id = @attachmentId
            """;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@attachmentId", attachmentId);

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var fileName = reader.GetTrimmedString("filename");
        var content = (byte[])reader["binaryblob"];
        return new AttachmentFile(fileName, content, GetContentType(fileName));
    }

    public async Task<AttachmentFile?> GetPurchaseOrderPdfAsync(string receiptNumber, string poNumber, CancellationToken cancellationToken = default)
    {
        const string sql = """
            select top 1 POBinary
            from nzbWFPPVTransactionLne
            where ReceiptNumber=@receiptNumber and PONumber=@poNumber
            """;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);
        command.Parameters.AddWithValue("@poNumber", poNumber);

        await connection.OpenAsync(cancellationToken);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        if (result is null or DBNull)
        {
            return null;
        }

        return new AttachmentFile(poNumber + ".pdf", (byte[])result, "application/pdf");
    }

    public async Task<bool> IsAdminUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        const string sql = "select ADUserID from nzbApprovalAdmins where Inactive='No'";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            if (string.Equals(reader.GetTrimmedString("ADUserID"), userId, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public async Task<string> GetManagerEmailAddressAsync(string managerId, CancellationToken cancellationToken = default)
    {
        const string sql = """
            select top 1 ManagerEmailAddress
            from nzbPPVSetup
            where ManagerADAccount=@managerId and Active='Yes' and ApprovalID='PPV1'
            """;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@managerId", managerId);
        await connection.OpenAsync(cancellationToken);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result == null || result == DBNull.Value ? string.Empty : Convert.ToString(result) ?? string.Empty;
    }

    private async Task<(string AccountName, string EmailAddress)> GetActivePpvManagerAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            select top 1 ManagerADAccount, ManagerEmailAddress
            from nzbPPVSetup
            where Active='Yes' and ApprovalID='PPV1'
            order by ManagerADAccount
            """;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);

        return await reader.ReadAsync(cancellationToken)
            ? (reader.GetTrimmedString("ManagerADAccount"), reader.GetTrimmedString("ManagerEmailAddress"))
            : (string.Empty, string.Empty);
    }

    private async Task<PpvDashboardItem?> GetDashboardItemAsync(string receiptNumber, UserContext user, CancellationToken cancellationToken)
    {
        var sql = """
            select ReceiptNumber,VendorID,VendorName,VendorDocNumber,SubmittedBy,SubmittedByEmailAddress,
                   SubmittedDateTime,Approvers,PurchaseOrders,ManagerApproval,ManagerApprovalDisplay,Comment,FirstLevelApprover
            from nzbWFPPVDashboard
            where ReceiptNumber=@receiptNumber
            """;

        if (!user.IsAdminUser)
        {
            sql += " and ReceiptNumber in (select ReceiptNumber from nzbWFPPVApproverList where AccountName=@userId)";
        }

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);
        command.Parameters.AddWithValue("@userId", user.UserId);

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ReadDashboardItem(reader) : null;
    }

    private async Task<IReadOnlyList<PpvLineItem>> GetLineItemsAsync(string receiptNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            select rtrim(ItemNumber) ItemNumber, rtrim(ItemDescription) ItemDescription,
                   rtrim(VendorItemNumber) VendorItemNumber, Variance, POCost, InvoiceCost,
                   rtrim(PONumber) PONumber, rtrim(ShipmentNumber) ShipmentNumber
            from nzbWFPPVTransactionLne
            where ReceiptNumber=@receiptNumber
            """;

        var items = new List<PpvLineItem>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PpvLineItem
            {
                ItemNumber = reader.GetTrimmedString("ItemNumber"),
                ItemDescription = reader.GetTrimmedString("ItemDescription"),
                VendorItemNumber = reader.GetTrimmedString("VendorItemNumber"),
                Variance = reader.GetDecimalOrDefault("Variance"),
                PoCost = reader.GetDecimalOrDefault("POCost"),
                InvoiceCost = reader.GetDecimalOrDefault("InvoiceCost"),
                PoNumber = reader.GetTrimmedString("PONumber"),
                ShipmentNumber = reader.GetTrimmedString("ShipmentNumber")
            });
        }

        return items;
    }

    private async Task<IReadOnlyList<PpvAttachment>> GetAttachmentsAsync(string receiptNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            select co3.filename, co1.attachment_id
            from CO00102 co1
            join CO00101 co3 on co3.Attachment_ID = co1.Attachment_ID
            join coAttachmentItems co2 on co1.Attachment_ID = co2.Attachment_ID
            where BusObjKey like @receiptSearch and DELETE1=0
            """;

        var items = new List<PpvAttachment>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptSearch", "%" + receiptNumber + "%");

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PpvAttachment
            {
                FileName = reader.GetTrimmedString("filename"),
                AttachmentId = reader.GetTrimmedString("attachment_id")
            });
        }

        return items;
    }

    private async Task<IReadOnlyList<PpvHistoryItem>> GetHistoryAsync(string receiptNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            select StatusDateTime,
                   rtrim(UserId) UserID,
                   case StatusId when 1 then 'Submitted' when 2 then 'Approved' when 3 then 'Rejected' else 'Reset' end as StatusText,
                   rtrim(Comment) Comment
            from nzbWFPPVInvoiceHistory
            where ReceiptNumber=@receiptNumber
            order by StatusDateTime desc
            """;

        var items = new List<PpvHistoryItem>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);

        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PpvHistoryItem
            {
                StatusDateTime = reader.GetDateTimeOrNull("StatusDateTime"),
                UserId = reader.GetTrimmedString("UserID"),
                Status = reader.GetTrimmedString("StatusText"),
                Comment = reader.GetTrimmedString("Comment")
            });
        }

        return items;
    }

    private async Task<string> GetPpvCommentAsync(string receiptNumber, CancellationToken cancellationToken)
    {
        const string sql = "select Comment from nzbWFPPVTransactionHdr where ReceiptNumber=@receiptNumber";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);
        await connection.OpenAsync(cancellationToken);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result == null || result == DBNull.Value ? string.Empty : Convert.ToString(result) ?? string.Empty;
    }

    private async Task<int> GetReceiptResetStatusAsync(string receiptNumber, CancellationToken cancellationToken)
    {
        const string sql = "select count(*) from nzbWFPPVInvoiceStatus where ReceiptNumber=@receiptNumber";
        return await ExecuteScalarIntAsync(sql, cancellationToken, new SqlParameter("@receiptNumber", receiptNumber));
    }

    private async Task<int> GetUserApprovalStatusAsync(string receiptNumber, UserContext user, CancellationToken cancellationToken)
    {
        var sql = user.IsAdminUser
            ? "select count(*) from nzbWFPPVApproverList where ReceiptNumber=@receiptNumber"
            : "select count(*) from nzbWFPPVApproverList where ReceiptNumber=@receiptNumber and AccountName=@userId";

        return await ExecuteScalarIntAsync(
            sql,
            cancellationToken,
            new SqlParameter("@receiptNumber", receiptNumber),
            new SqlParameter("@userId", user.UserId));
    }

    private async Task<int> ExecuteScalarIntAsync(string sql, CancellationToken cancellationToken, params SqlParameter[] parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        await connection.OpenAsync(cancellationToken);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
    }

    private async Task ExecuteStoredProcedureAsync(string name, CancellationToken cancellationToken, params SqlParameter[] parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        var parameterList = string.Join(",", parameters.Select(parameter => parameter.ParameterName));
        using var command = new SqlCommand("set xact_abort on; execute " + name + " " + parameterList, connection)
        {
            CommandType = CommandType.Text,
            CommandTimeout = 600
        };

        command.Parameters.AddRange(parameters);
        await connection.OpenAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();
        command.Transaction = transaction;

        try
        {
            await command.ExecuteNonQueryAsync(cancellationToken);
            transaction.Commit();
        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch
            {
                // Preserve the original SQL exception for the UI.
            }

            throw;
        }
    }

    private static PpvDashboardItem ReadDashboardItem(SqlDataReader reader)
    {
        return new PpvDashboardItem
        {
            ReceiptNumber = reader.GetTrimmedString("ReceiptNumber"),
            VendorId = reader.GetTrimmedString("VendorID"),
            VendorName = reader.GetTrimmedString("VendorName"),
            VendorDocNumber = reader.GetTrimmedString("VendorDocNumber"),
            SubmittedBy = reader.GetTrimmedString("SubmittedBy"),
            SubmittedByEmailAddress = reader.GetTrimmedString("SubmittedByEmailAddress"),
            SubmittedDateTime = reader.GetDateTimeOrNull("SubmittedDateTime"),
            Approvers = reader.GetTrimmedString("Approvers"),
            PurchaseOrders = reader.GetTrimmedString("PurchaseOrders"),
            ManagerApproval = reader.GetInt32OrDefault("ManagerApproval"),
            ManagerApprovalDisplay = reader.GetTrimmedString("ManagerApprovalDisplay"),
            Comment = reader.GetTrimmedString("Comment"),
            FirstLevelApprover = reader.GetTrimmedString("FirstLevelApprover")
        };
    }

    private static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".xls" => "application/vnd.ms-excel",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".doc" => "application/msword",
            ".msg" => "application/vnd.ms-outlook",
            _ => "application/octet-stream"
        };
    }
}
