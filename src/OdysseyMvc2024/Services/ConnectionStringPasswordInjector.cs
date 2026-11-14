// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionStringPasswordInjector.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Injects SQL password values from protected files into configured connection strings.
//   Development-only: skips injection on staging/production where password file is not deployed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc2024.Services;

internal static class ConnectionStringPasswordInjector
{
    private const string PasswordMarker = "Password=";

    /// <summary>
    /// Injects SQL password from a protected file into the connection string.
    /// On staging/production where the protected/ directory is not deployed,
    /// gracefully skips injection and returns the connection string unchanged
    /// (assumes the password placeholder is already filled via environment variable).
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
        
        // On staging/production, the protected/ directory is not deployed.
        // If the file doesn't exist, assume the password is already filled via environment variable.
        if (!File.Exists(passwordFilePath))
        {
            return connectionString;
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
