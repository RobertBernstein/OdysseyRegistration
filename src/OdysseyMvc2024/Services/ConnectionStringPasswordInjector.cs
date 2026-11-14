// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionStringPasswordInjector.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Injects SQL password values from protected files into configured connection strings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc2024.Services;

internal static class ConnectionStringPasswordInjector
{
    private const string PasswordMarker = "Password=";

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
            throw new InvalidOperationException($"Required password file was not found at '{passwordFilePath}'.");
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
