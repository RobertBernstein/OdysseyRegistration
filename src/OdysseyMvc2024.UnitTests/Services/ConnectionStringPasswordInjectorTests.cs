using FluentAssertions;
using OdysseyMvc2024.Services;

namespace OdysseyMvc2024.UnitTests.Services;

public sealed class ConnectionStringPasswordInjectorTests
{
    [Fact]
    public void InjectSqlPasswordFromProtectedFile_WhenPasswordSegmentExists_ReplacesPasswordValue()
    {
        // Arrange
        const string connectionString = "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=placeholder;TrustServerCertificate=True;";
        const string expected = "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=super-secret;TrustServerCertificate=True;";

        string contentRootPath = CreateContentRootPath();
        WritePasswordFile(contentRootPath, "  super-secret  ");

        try
        {
            // Act
            string updated = ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile(connectionString, contentRootPath);

            // Assert
            updated.Should().Be(expected);
        }
        finally
        {
            DeleteDirectory(contentRootPath);
        }
    }

    [Fact]
    public void InjectSqlPasswordFromProtectedFile_WhenPasswordIsLastEntry_ReplacesToEndOfString()
    {
        // Arrange
        const string connectionString = "Server=localhost;Password=old-password";
        const string expected = "Server=localhost;Password=new-password";

        string contentRootPath = CreateContentRootPath();
        WritePasswordFile(contentRootPath, "new-password");

        try
        {
            // Act
            string updated = ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile(connectionString, contentRootPath);

            // Assert
            updated.Should().Be(expected);
        }
        finally
        {
            DeleteDirectory(contentRootPath);
        }
    }

    [Fact]
    public void InjectSqlPasswordFromProtectedFile_WhenConnectionStringMissingPassword_ThrowsInvalidOperationException()
    {
        // Arrange
        const string connectionString = "Server=localhost;Database=DB_12824_registration;User Id=sa;";

        string contentRootPath = CreateContentRootPath();
        WritePasswordFile(contentRootPath, "secret");

        try
        {
            // Act
            Action action = () => ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile(connectionString, contentRootPath);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
        finally
        {
            DeleteDirectory(contentRootPath);
        }
    }

    [Fact]
    public void InjectSqlPasswordFromProtectedFile_WhenPasswordFileMissing_ThrowsInvalidOperationException()
    {
        // Arrange
        const string connectionString = "Server=localhost;Password=placeholder;";
        string contentRootPath = CreateContentRootPath();

        try
        {
            // Act
            Action action = () => ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile(connectionString, contentRootPath);

            // Assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("*password file was not found*");
        }
        finally
        {
            DeleteDirectory(contentRootPath);
        }
    }

    [Fact]
    public void InjectSqlPasswordFromProtectedFile_WhenPasswordFileIsWhitespace_ThrowsInvalidOperationException()
    {
        // Arrange
        const string connectionString = "Server=localhost;Password=placeholder;";

        string contentRootPath = CreateContentRootPath();
        WritePasswordFile(contentRootPath, "   \r\n ");

        try
        {
            // Act
            Action action = () => ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile(connectionString, contentRootPath);

            // Assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("*is empty*");
        }
        finally
        {
            DeleteDirectory(contentRootPath);
        }
    }

    private static string CreateContentRootPath()
    {
        string path = Path.Combine(Path.GetTempPath(), $"odyssey-password-injector-{Guid.NewGuid():N}");
        Directory.CreateDirectory(path);
        return path;
    }

    private static void WritePasswordFile(string contentRootPath, string passwordContent)
    {
        string protectedDirectory = Path.Combine(contentRootPath, "protected");
        Directory.CreateDirectory(protectedDirectory);
        File.WriteAllText(Path.Combine(protectedDirectory, "sql-pw.txt"), passwordContent);
    }

    private static void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive: true);
        }
    }
}
