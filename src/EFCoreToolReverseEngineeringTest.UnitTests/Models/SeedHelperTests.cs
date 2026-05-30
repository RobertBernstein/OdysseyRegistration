using FluentAssertions;
using JudgesRegistrationRazorDb.Models;

namespace EFCoreToolReverseEngineeringTest.UnitTests;

public class SeedHelperTests
{
    private class TestEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    [Fact]
    public void SeedData_ValidFileWithValidJson_ReturnsDeserializedList()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-valid.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "[{\"Id\":1,\"Name\":\"Test1\"},{\"Id\":2,\"Name\":\"Test2\"}]";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result![0].Id.Should().Be(1);
            result[0].Name.Should().Be("Test1");
            result[1].Id.Should().Be(2);
            result[1].Name.Should().Be("Test2");
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_FileNotFound_ThrowsFileNotFoundException()
    {
        // Arrange
        string fileName = "non-existent-file.json";

        // Act
        Action act = () => SeedHelper.SeedData<TestEntity>(fileName);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void SeedData_InvalidJson_ThrowsJsonException()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-invalid.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "{ invalid json content }";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            Action act = () => SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            act.Should().Throw<Newtonsoft.Json.JsonReaderException>();
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_EmptyJsonArray_ReturnsEmptyList()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-empty.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "[]";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_NullJsonValue_ReturnsNull()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-null.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "null";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().BeNull();
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_JsonWithWhitespace_ReturnsDeserializedList()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-whitespace.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = @"
        [
            {
                ""Id"": 1,
                ""Name"": ""Test1""
            },
            {
                ""Id"": 2,
                ""Name"": ""Test2""
            }
        ]
        ";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result![0].Id.Should().Be(1);
            result[0].Name.Should().Be("Test1");
            result[1].Id.Should().Be(2);
            result[1].Name.Should().Be("Test2");
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_EmptyFile_ReturnsNull()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-empty-file.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        File.WriteAllText(filePath, string.Empty);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().BeNull();
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_PartiallyMatchingJson_DeserializesWithDefaults()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-partial.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "[{\"Id\":1},{\"Name\":\"Test2\"}]";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(fileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result![0].Id.Should().Be(1);
            result[0].Name.Should().BeNull();
            result[1].Id.Should().Be(0);
            result[1].Name.Should().Be("Test2");
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void SeedData_DifferentEntityType_SuccessfullyDeserializes()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);
        
        string fileName = "test-string.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        string jsonContent = "[\"value1\",\"value2\",\"value3\"]";
        File.WriteAllText(filePath, jsonContent);

        try
        {
            // Act
            var result = SeedHelper.SeedData<string>(fileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result![0].Should().Be("value1");
            result[1].Should().Be("value2");
            result[2].Should().Be("value3");
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
