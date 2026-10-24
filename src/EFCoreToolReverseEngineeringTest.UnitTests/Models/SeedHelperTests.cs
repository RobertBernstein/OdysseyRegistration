using FluentAssertions;
using JudgesRegistrationRazorDb.Models;
using Newtonsoft.Json;

namespace EFCoreToolReverseEngineeringTest.UnitTests.Models;

public class SeedHelperTests
{
    [method: JsonConstructor]
    private sealed class TestEntity(int id = 0, string? name = null)
    {
        public int Id { get; } = id;
        public string? Name { get; } = name;
    }

    [Fact]
    public void SeedData_ValidFileWithValidJson_ReturnsDeserializedList()
    {
        // Arrange
        string testDirectory = Directory.GetCurrentDirectory();
        string seedDataPath = Path.Combine(testDirectory, "SeedData");
        Directory.CreateDirectory(seedDataPath);

        const string fileName = "test-valid.json";
        string filePath = Path.Combine(seedDataPath, fileName);

        var expectedData = new List<TestEntity>
        {
            new(1, "Test1"),
            new(2, "Test2")
        };

        string jsonContent = JsonConvert.SerializeObject(expectedData);
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
        const string fileName = "non-existent-file.json";

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

        const string fileName = "test-invalid.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = "{ invalid json content }";
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
        
        const string fileName = "test-empty.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = "[]";
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

        const string fileName = "test-null.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = "null";
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

        const string fileName = "test-whitespace.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = @"
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

        const string fileName = "test-empty-file.json";
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

        const string fileName = "test-partial.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = "[{\"Id\":1},{\"Name\":\"Test2\"}]";
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

        const string fileName = "test-string.json";
        string filePath = Path.Combine(seedDataPath, fileName);
        const string jsonContent = "[\"value1\",\"value2\",\"value3\"]";
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
