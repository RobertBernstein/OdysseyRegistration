<#
.SYNOPSIS
    Publish the OdysseyRegistration project to a specified environment (test or prod).
.DESCRIPTION
    This script performs the following steps:
    1. Reads configuration from publish-config.json to determine paths and credentials.
    2. Optionally executes 'dotnet publish' to build the project into a local output directory.
    3. Uses the WinSCP .NET assembly to upload the published files to the remote server via FTPS.
.PARAMETER Environment
    The target environment to publish to. Valid values are "test" and "prod".
.PARAMETER SkipPublish
    If specified, skips the 'dotnet publish' step and uploads the existing output directory as-is.
    Useful for re-deploying after a failed upload without rebuilding the project.
.EXAMPLE
    .\publish.ps1 -Environment test
    Builds and deploys to the test environment defined in publish-config.json.
.EXAMPLE
    .\publish.ps1 -Environment prod -SkipPublish
    Uploads the existing publish output to production without rebuilding.
.NOTES
    - publish-config.json must be present in the same directory as this script.
    - WinSCP .NET assembly path is configured in publish-config.json under global.winscpAssembly.
    - FTP passwords are resolved from environment variables at runtime using %VAR_NAME% placeholders.
    - Set credentials once per machine (persists in user profile):
        [System.Environment]::SetEnvironmentVariable("TEST_FTP_PASS", "yourTestPassword", "User")
        [System.Environment]::SetEnvironmentVariable("PROD_FTP_PASS",  "yourProdPassword",  "User")
#>
param (
    [Parameter(Mandatory=$true)]
    [ValidateSet("test", "prod")]
    [string]$Environment,

    [switch]$SkipPublish
)

$configFile = Join-Path $PSScriptRoot "publish-config.json"
if (-not (Test-Path $configFile)) { Write-Error "Config not found: $configFile"; exit 1 }

$config    = Get-Content $configFile | ConvertFrom-Json
$envConfig = $config.environments.$Environment
$globals   = $config.global

if (-not $envConfig) { Write-Error "Unknown environment: $Environment"; exit 1 }

# Resolve any %ENV_VAR% placeholders in the password
$password = [System.Environment]::ExpandEnvironmentVariables($envConfig.ftpPass)
if ($password -match '^%.*%$') {
    Write-Error "Password environment variable not set: $($envConfig.ftpPass)"
    exit 1
}

# --- Step 1: dotnet publish ---
if ($SkipPublish) {
    Write-Host "=== Skipping dotnet publish [$Environment] ===" -ForegroundColor Yellow
    if (-not (Test-Path $envConfig.publishDir)) {
        Write-Error "Publish directory not found: $($envConfig.publishDir). Run without -SkipPublish first."
        exit 1
    }
} else {
    Write-Host "=== Publishing [$Environment] ===" -ForegroundColor Cyan

    if (Test-Path $envConfig.publishDir) { Remove-Item $envConfig.publishDir -Recurse -Force }

    dotnet publish $envConfig.projectPath -c Release -o $envConfig.publishDir
    if ($LASTEXITCODE -ne 0) { Write-Error "dotnet publish failed"; exit $LASTEXITCODE }
}

# --- Step 2: FTPS upload via WinSCP ---
Write-Host "=== Uploading to $($envConfig.ftpHost) ===" -ForegroundColor Cyan

# Load WinSCP .NET assembly
Add-Type -Path $globals.winscpAssembly

$tlsfingerprint = if ($Environment -eq "prod") { $globals.prodTlsFingerprint } else { $globals.testTlsFingerprint }

# Set up session options
$sessionOptions = New-Object WinSCP.SessionOptions -Property @{
    Protocol   = [WinSCP.Protocol]::Ftp
    FtpSecure  = [WinSCP.FtpSecure]::Explicit
    HostName   = $envConfig.ftpHost
    PortNumber = 21
    UserName   = $envConfig.ftpUser
    Password   = $password
    TlsHostCertificateFingerprint = $tlsfingerprint  # SHA-1 or SHA-256 of the TLS cert
}

$uploadTimer = [System.Diagnostics.Stopwatch]::StartNew()

$session = New-Object WinSCP.Session

try {
    # Connect
    $session.Open($sessionOptions)

    # Ensure remote directory exists
    if (-not $session.FileExists($envConfig.remotePath)) {
        $session.CreateDirectory($envConfig.remotePath)
    }

    # Set up transfer options
    $transferOptions = New-Object WinSCP.TransferOptions
    $transferOptions.TransferMode = [WinSCP.TransferMode]::Binary
    $transferOptions.AddRawSettings("NewerOnly", "1")

    # This works!
    # $session.GetFiles("$($env.remotePath)*", $env.publishDir, $false, $transferOptions).Check()

    # Transfer files
    $result = $session.PutFiles(
        (Join-Path $envConfig.publishDir "*"),  # local path with wildcard
        "$($envConfig.remotePath)*", # remote path with wildcard (ensures files are placed in the correct directory)
        $false,   # $false = do not delete source files after upload
        $transferOptions
    )

    $result.Check()
    $uploadTimer.Stop()

    Write-Host "=== Deployed to [$Environment] successfully ===" -ForegroundColor Green
    Write-Host "Upload time: $($uploadTimer.Elapsed.ToString('mm\:ss\.ff'))" -ForegroundColor Green
}
catch {
    Write-Error "Upload failed: $($_.Exception.Message)"
    exit 1
}
finally {
    $session.Dispose()
}
