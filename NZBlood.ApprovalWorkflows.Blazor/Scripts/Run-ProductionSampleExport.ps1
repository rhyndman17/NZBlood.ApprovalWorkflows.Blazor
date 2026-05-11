[CmdletBinding()]
param(
    [string] $ConnectionString = $env:NZBLOOD_APPROVALWORKFLOW_CONNECTIONSTRING,

    [string] $OutputPath,

    [int] $SampleRows = 25,

    [string] $ReceiptNumber,

    [string] $UserId,

    [switch] $NoCsv,

    [switch] $KeepExistingOutput
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Parent $PSCommandPath
$projectRoot = Split-Path -Parent $scriptRoot
$exporter = Join-Path $scriptRoot "Export-ProductionSampleData.ps1"

if ([string]::IsNullOrWhiteSpace($ConnectionString)) {
    throw @"
Connection string was not provided.

Set it for this PowerShell session:
  `$env:NZBLOOD_APPROVALWORKFLOW_CONNECTIONSTRING = "<connection string>"

Or pass it directly:
  .\Scripts\Run-ProductionSampleExport.ps1 -ConnectionString "<connection string>"
"@
}

if ([string]::IsNullOrWhiteSpace($OutputPath)) {
    $OutputPath = Join-Path $projectRoot "ExportedSamples"
}

if (-not $KeepExistingOutput -and (Test-Path -LiteralPath $OutputPath)) {
    Write-Host "Cleaning previous export files from $OutputPath..."
    Get-ChildItem -LiteralPath $OutputPath -File -Include "*.json", "*.csv" |
        Remove-Item -Force
}

$arguments = @{
    ConnectionString = $ConnectionString
    OutputPath = $OutputPath
    SampleRows = $SampleRows
}

if (-not [string]::IsNullOrWhiteSpace($ReceiptNumber)) {
    $arguments.ReceiptNumber = $ReceiptNumber
}

if (-not [string]::IsNullOrWhiteSpace($UserId)) {
    $arguments.UserId = $UserId
}

if (-not $NoCsv) {
    $arguments.Csv = $true
}

& $exporter @arguments
