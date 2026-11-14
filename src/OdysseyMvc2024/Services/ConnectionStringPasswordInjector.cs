// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionStringPasswordInjector.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Injects SQL password values from protected files into configured connection strings.
//   Development: File is auto-created at build time. Staging/Production: File must be uploaded via FTPS.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc2024.Services;

internal static class ConnectionStringPasswordInjector
{
    private const string PasswordMarker = "Password=";

    /// <summary>
    /// Injects SQL password from a protected file into the connection string.
    /// The password file must always exist before startup:
    /// - Development: Auto-created at build time by CopyPasswordFile MSBuild target
    /// - Staging/Production: Must be uploaded via FTPS before application starts
    /// </summary>
    internal static string InjectSqlPasswordFromProtectedFile(string connectionString, string contentRootPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentRootPath);

        int markerIndex = connectionString.IndexOf(PasswordMarker, StringComparison.OrdinalIgnoreCase);
        if (markerIndex < 0)
        {
            throw new InvalidOperationException("ConnectionStrings:OdysseyConnection must contain Password=.");
        }

        string passwordFilePath = Path.Combine(contentRootPath, "protected", "sql-pw.txt");
        if (!File.Exists(passwordFilePath))
        {
            throw new InvalidOperationException($"Required password file was not found at '{passwordFilePath}'. " +
                "Development: Rebuild the project to auto-create this file. " +
                "Staging/Production: Upload protected/sql-pw.txt via FTPS before starting the application.");
        }

        string passwordFromFile = File.ReadAllText(passwordFilePath).Trim();
        if (string.IsNullOrWhiteSpace(passwordFromFile))
        {
            throw new InvalidOperationException($"The password file '{passwordFilePath}' is empty.");
        }

        int passwordValueStart = markerIndex + PasswordMarker.Length;
        int passwordValueEnd = connectionString.IndexOf(';', passwordValueStart);
        if (passwordValueEnd < 0)
        {
            passwordValueEnd = connectionString.Length;
        }

        return string.Concat(
            connectionString.AsSpan(0, passwordValueStart),
            passwordFromFile,
            connectionString.AsSpan(passwordValueEnd));
    }
}
