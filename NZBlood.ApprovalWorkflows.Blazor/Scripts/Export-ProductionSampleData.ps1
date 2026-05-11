[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $ConnectionString,

    [string] $OutputPath = ".\ExportedSamples",

    [int] $SampleRows = 25,

    [string] $ReceiptNumber,

    [string] $UserId,

    [switch] $Csv
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Data

function New-SqlConnection {
    $connection = [System.Data.SqlClient.SqlConnection]::new($ConnectionString)
    $connection.Open()
    return $connection
}

function Invoke-DataTable {
    param(
        [Parameter(Mandatory = $true)]
        [System.Data.SqlClient.SqlConnection] $Connection,

        [Parameter(Mandatory = $true)]
        [string] $Sql,

        [hashtable] $Parameters = @{}
    )

    $command = $Connection.CreateCommand()
    $command.CommandText = $Sql
    $command.CommandTimeout = 600

    foreach ($key in $Parameters.Keys) {
        $parameter = [System.Data.SqlClient.SqlParameter]::new()
        $parameter.ParameterName = "@$key"
        $parameter.SqlDbType = [System.Data.SqlDbType]::NVarChar
        $parameter.Size = 4000
        $parameter.Value = if ($null -eq $Parameters[$key]) { [DBNull]::Value } else { [string] $Parameters[$key] }
        [void] $command.Parameters.Add($parameter)
    }

    $table = [System.Data.DataTable]::new()
    $adapter = [System.Data.SqlClient.SqlDataAdapter]::new($command)
    [void] $adapter.Fill($table)
    return ,$table
}

function ConvertTo-PlainRows {
    param([Parameter(Mandatory = $true)] [System.Data.DataTable] $Table)

    $rows = for ($rowIndex = 0; $rowIndex -lt $Table.Rows.Count; $rowIndex++) {
        $dataRow = $Table.Rows[$rowIndex]
        $item = [ordered] @{}
        for ($columnIndex = 0; $columnIndex -lt $Table.Columns.Count; $columnIndex++) {
            $dataColumn = $Table.Columns[$columnIndex]
            $value = $dataRow.Item($dataColumn)
            $item[$dataColumn.ColumnName] = if ([DBNull]::Value.Equals($value)) { $null } else { $value }
        }

        [pscustomobject] $item
    }

    return @($rows)
}

function Export-DataSet {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Name,

        [Parameter(Mandatory = $true)]
        [System.Data.DataTable] $Table
    )

    Write-Host "Writing $Name ($($Table.Rows.Count) rows)..."

    try {
        $rows = @(ConvertTo-PlainRows -Table $Table)
        $jsonPath = Join-Path $OutputPath "$Name.json"
        ConvertTo-Json -InputObject @($rows) -Depth 8 | Set-Content -Path $jsonPath -Encoding UTF8

        if ($Csv) {
            $csvPath = Join-Path $OutputPath "$Name.csv"
            $rows | Export-Csv -Path $csvPath -NoTypeInformation -Encoding UTF8
        }

        return [pscustomobject] @{
            Name = $Name
            RowCount = $Table.Rows.Count
            JsonFile = $jsonPath
            CsvFile = if ($Csv) { Join-Path $OutputPath "$Name.csv" } else { $null }
        }
    }
    catch {
        throw "Failed while writing dataset '$Name': $($_.Exception.Message)"
    }
}

function Get-ObjectSampleSql {
    param(
        [Parameter(Mandatory = $true)]
        [System.Data.SqlClient.SqlConnection] $Connection,

        [Parameter(Mandatory = $true)]
        [string] $ObjectName
    )

    $schemaSql = @"
select
    schema_name(o.schema_id) as SchemaName,
    o.name as ObjectName,
    c.name as ColumnName,
    t.name as TypeName
from sys.objects o
join sys.columns c on c.object_id = o.object_id
join sys.types t on t.user_type_id = c.user_type_id
where o.name = @ObjectName
order by c.column_id
"@

    $columns = Invoke-DataTable -Connection $Connection -Sql $schemaSql -Parameters @{ ObjectName = $ObjectName }
    if ($columns.Rows.Count -eq 0) {
        Write-Warning "Object '$ObjectName' was not found; skipping."
        return $null
    }

    $schema = $columns.Rows[0]["SchemaName"]
    $object = $columns.Rows[0]["ObjectName"]
    $selectColumns = foreach ($column in $columns.Rows) {
        $name = [string] $column["ColumnName"]
        $type = ([string] $column["TypeName"]).ToLowerInvariant()
        $quoted = "[$($name.Replace("]", "]]"))]"

        if ($type -in @("binary", "varbinary", "image", "timestamp", "rowversion")) {
            "DATALENGTH($quoted) as [$($name)__Bytes], lower(convert(varchar(64), hashbytes('SHA2_256', substring(convert(varbinary(max), $quoted), 1, 8000)), 2)) as [$($name)__First8000Sha256]"
        }
        else {
            $quoted
        }
    }

    $qualifiedName = "[$($schema.Replace("]", "]]"))].[$($object.Replace("]", "]]"))]"
    return "select top ($SampleRows) $($selectColumns -join ", ") from $qualifiedName"
}

function Export-ObjectSchema {
    param([Parameter(Mandatory = $true)] [System.Data.SqlClient.SqlConnection] $Connection)

    $sql = @"
select
    schema_name(o.schema_id) as SchemaName,
    o.name as ObjectName,
    o.type_desc as ObjectType,
    c.column_id as ColumnOrdinal,
    c.name as ColumnName,
    t.name as SqlType,
    c.max_length as MaxLength,
    c.precision as Precision,
    c.scale as Scale,
    c.is_nullable as IsNullable
from sys.objects o
join sys.columns c on c.object_id = o.object_id
join sys.types t on t.user_type_id = c.user_type_id
where o.name in (
    'nzbWFPPVDashboard',
    'nzbWFPPVApproverList',
    'nzbWFPPVTransactionLne',
    'nzbWFPPVTransactionHdr',
    'nzbWFPPVInvoiceHistory',
    'nzbWFPPVInvoiceStatus',
    'nzbPPVSetup',
    'nzbApprovalAdmins',
    'CO00101',
    'CO00102',
    'coAttachmentItems'
)
order by ObjectName, ColumnOrdinal
"@

    $table = Invoke-DataTable -Connection $Connection -Sql $sql
    Export-DataSet -Name "_schema" -Table $table
}

function Export-Query {
    param(
        [Parameter(Mandatory = $true)] [System.Data.SqlClient.SqlConnection] $Connection,
        [Parameter(Mandatory = $true)] [string] $Name,
        [Parameter(Mandatory = $true)] [string] $Sql,
        [hashtable] $Parameters = @{}
    )

    Write-Host "Exporting $Name..."

    try {
        $table = Invoke-DataTable -Connection $Connection -Sql $Sql -Parameters $Parameters
        Export-DataSet -Name $Name -Table $table
    }
    catch {
        $message = @"
Failed while exporting '$Name'.

Error:
$($_.Exception.Message)

SQL:
$Sql
"@
        throw $message
    }
}

New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
$connection = New-SqlConnection

try {
    $manifest = @()
    $manifest += Export-ObjectSchema -Connection $connection

    $sourceObjects = @(
        "nzbWFPPVDashboard",
        "nzbWFPPVApproverList",
        "nzbWFPPVTransactionLne",
        "nzbWFPPVTransactionHdr",
        "nzbWFPPVInvoiceHistory",
        "nzbWFPPVInvoiceStatus",
        "nzbPPVSetup",
        "nzbApprovalAdmins",
        "CO00101",
        "CO00102",
        "coAttachmentItems"
    )

    foreach ($objectName in $sourceObjects) {
        $sql = Get-ObjectSampleSql -Connection $connection -ObjectName $objectName
        if ($null -ne $sql) {
            $manifest += Export-Query -Connection $connection -Name "source_$objectName" -Sql $sql
        }
    }

    $appQueries = @(
        @{
            Name = "app_dashboard"
            Sql = @"
select top ($SampleRows)
    ReceiptNumber, VendorID, VendorName, VendorDocNumber, SubmittedBy, SubmittedByEmailAddress,
    SubmittedDateTime, Approvers, PurchaseOrders, ManagerApproval, ManagerApprovalDisplay, Comment, FirstLevelApprover
from nzbWFPPVDashboard
order by ReceiptNumber
"@
            Parameters = @{}
        },
        @{
            Name = "app_approvers"
            Sql = "select top ($SampleRows) * from nzbWFPPVApproverList order by ReceiptNumber"
            Parameters = @{}
        },
        @{
            Name = "app_admins"
            Sql = "select top ($SampleRows) ADUserID, Inactive from nzbApprovalAdmins order by ADUserID"
            Parameters = @{}
        },
        @{
            Name = "app_managers"
            Sql = @"
select top ($SampleRows) ApprovalID, ManagerADAccount, ManagerEmailAddress, Active
from nzbPPVSetup
order by ApprovalID, ManagerADAccount
"@
            Parameters = @{}
        }
    )

    if (-not [string]::IsNullOrWhiteSpace($UserId)) {
        $appQueries += @{
            Name = "app_dashboard_for_user"
            Sql = @"
select
    ReceiptNumber, VendorID, VendorName, VendorDocNumber, SubmittedBy, SubmittedByEmailAddress,
    SubmittedDateTime, Approvers, PurchaseOrders, ManagerApproval, ManagerApprovalDisplay, Comment, FirstLevelApprover
from nzbWFPPVDashboard
where ReceiptNumber in (select ReceiptNumber from nzbWFPPVApproverList where AccountName = @UserId)
order by ReceiptNumber
"@
            Parameters = @{ UserId = $UserId }
        }
    }

    if ([string]::IsNullOrWhiteSpace($ReceiptNumber)) {
        $appQueries += @(
            @{
                Name = "app_receipt_header"
                Sql = @"
select top ($SampleRows)
    ReceiptNumber, VendorID, VendorName, VendorDocNumber, SubmittedBy, SubmittedByEmailAddress,
    SubmittedDateTime, Approvers, PurchaseOrders, ManagerApproval, ManagerApprovalDisplay, Comment, FirstLevelApprover
from nzbWFPPVDashboard
order by ReceiptNumber
"@
                Parameters = @{}
            },
            @{
                Name = "app_receipt_lines"
                Sql = @"
select top ($SampleRows)
    ReceiptNumber,
    rtrim(ItemNumber) as ItemNumber,
    rtrim(ItemDescription) as ItemDescription,
    rtrim(VendorItemNumber) as VendorItemNumber,
    Variance,
    POCost,
    InvoiceCost,
    rtrim(PONumber) as PONumber,
    rtrim(ShipmentNumber) as ShipmentNumber,
    DATALENGTH(POBinary) as POBinary__Bytes,
    lower(convert(varchar(64), hashbytes('SHA2_256', substring(convert(varbinary(max), POBinary), 1, 8000)), 2)) as POBinary__First8000Sha256
from nzbWFPPVTransactionLne
order by ReceiptNumber, PONumber, ShipmentNumber, ItemNumber
"@
                Parameters = @{}
            },
            @{
                Name = "app_receipt_attachments"
                Sql = @"
select top ($SampleRows)
    co1.BusObjKey,
    co3.filename,
    co1.attachment_id,
    DATALENGTH(co2.binaryblob) as binaryblob__Bytes,
    lower(convert(varchar(64), hashbytes('SHA2_256', substring(convert(varbinary(max), co2.binaryblob), 1, 8000)), 2)) as binaryblob__First8000Sha256
from CO00102 co1
join CO00101 co3 on co3.Attachment_ID = co1.Attachment_ID
join coAttachmentItems co2 on co1.Attachment_ID = co2.Attachment_ID
where co1.DELETE1 = 0
order by co1.BusObjKey, co3.filename
"@
                Parameters = @{}
            },
            @{
                Name = "app_receipt_history"
                Sql = @"
select top ($SampleRows)
    ReceiptNumber,
    StatusDateTime,
    rtrim(UserId) as UserID,
    case StatusId when 1 then 'Submitted' when 2 then 'Approved' when 3 then 'Rejected' else 'Reset' end as StatusText,
    rtrim(Comment) as Comment,
    StatusId
from nzbWFPPVInvoiceHistory
order by ReceiptNumber, StatusDateTime desc
"@
                Parameters = @{}
            },
            @{
                Name = "app_receipt_transaction_comment"
                Sql = "select top ($SampleRows) ReceiptNumber, Comment from nzbWFPPVTransactionHdr order by ReceiptNumber"
                Parameters = @{}
            },
            @{
                Name = "app_receipt_status"
                Sql = "select top ($SampleRows) * from nzbWFPPVInvoiceStatus order by ReceiptNumber"
                Parameters = @{}
            }
        )
    }
    else {
        $appQueries += @(
            @{
                Name = "app_receipt_header"
                Sql = @"
select
    ReceiptNumber, VendorID, VendorName, VendorDocNumber, SubmittedBy, SubmittedByEmailAddress,
    SubmittedDateTime, Approvers, PurchaseOrders, ManagerApproval, ManagerApprovalDisplay, Comment, FirstLevelApprover
from nzbWFPPVDashboard
where ReceiptNumber = @ReceiptNumber
"@
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            },
            @{
                Name = "app_receipt_lines"
                Sql = @"
select
    rtrim(ItemNumber) as ItemNumber,
    rtrim(ItemDescription) as ItemDescription,
    rtrim(VendorItemNumber) as VendorItemNumber,
    Variance,
    POCost,
    InvoiceCost,
    rtrim(PONumber) as PONumber,
    rtrim(ShipmentNumber) as ShipmentNumber,
    DATALENGTH(POBinary) as POBinary__Bytes,
    lower(convert(varchar(64), hashbytes('SHA2_256', substring(convert(varbinary(max), POBinary), 1, 8000)), 2)) as POBinary__First8000Sha256
from nzbWFPPVTransactionLne
where ReceiptNumber = @ReceiptNumber
"@
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            },
            @{
                Name = "app_receipt_attachments"
                Sql = @"
select
    co3.filename,
    co1.attachment_id,
    DATALENGTH(co2.binaryblob) as binaryblob__Bytes,
    lower(convert(varchar(64), hashbytes('SHA2_256', substring(convert(varbinary(max), co2.binaryblob), 1, 8000)), 2)) as binaryblob__First8000Sha256
from CO00102 co1
join CO00101 co3 on co3.Attachment_ID = co1.Attachment_ID
join coAttachmentItems co2 on co1.Attachment_ID = co2.Attachment_ID
where co1.BusObjKey like '%' + @ReceiptNumber + '%' and co1.DELETE1 = 0
"@
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            },
            @{
                Name = "app_receipt_history"
                Sql = @"
select
    StatusDateTime,
    rtrim(UserId) as UserID,
    case StatusId when 1 then 'Submitted' when 2 then 'Approved' when 3 then 'Rejected' else 'Reset' end as StatusText,
    rtrim(Comment) as Comment,
    StatusId
from nzbWFPPVInvoiceHistory
where ReceiptNumber = @ReceiptNumber
order by StatusDateTime desc
"@
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            },
            @{
                Name = "app_receipt_transaction_comment"
                Sql = "select ReceiptNumber, Comment from nzbWFPPVTransactionHdr where ReceiptNumber = @ReceiptNumber"
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            },
            @{
                Name = "app_receipt_status"
                Sql = "select * from nzbWFPPVInvoiceStatus where ReceiptNumber = @ReceiptNumber"
                Parameters = @{ ReceiptNumber = $ReceiptNumber }
            }
        )
    }

    foreach ($query in $appQueries) {
        $manifest += Export-Query -Connection $connection -Name $query.Name -Sql $query.Sql -Parameters $query.Parameters
    }

    $manifestPath = Join-Path $OutputPath "_manifest.json"
    Write-Host "Writing _manifest ($($manifest.Count) entries)..."
    ConvertTo-Json -InputObject @($manifest) -Depth 6 | Set-Content -Path $manifestPath -Encoding UTF8

    Write-Host "Export complete: $((Resolve-Path $OutputPath).Path)"
    Write-Host "Manifest: $manifestPath"
}
finally {
    $connection.Dispose()
}
