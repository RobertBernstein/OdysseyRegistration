using System.Text;
using FluentAssertions;
using JudgesRegistrationRazor.Models;
using Newtonsoft.Json;

namespace JudgeRegistrationRazor.UnitTests.Models;

public class SeedHelperTests
{
    private class TestEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    [Fact]
    public void SeedData_WithValidFile_ReturnsDeserializedList()
    {
        // Arrange
        string testFileName = "test-data.json";
        var expectedData = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test2" }
        };
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        string json = JsonConvert.SerializeObject(expectedData);
        File.WriteAllText(fullPath, json);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

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
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithEmptyJsonArray_ReturnsEmptyList()
    {
        // Arrange
        string testFileName = "empty-data.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(fullPath, "[]");

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithNullJsonValue_ReturnsNull()
    {
        // Arrange
        string testFileName = "null-data.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(fullPath, "null");

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().BeNull();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithEmptyFile_ReturnsNull()
    {
        // Arrange
        string testFileName = "empty-file.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(fullPath, string.Empty);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().BeNull();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithNonExistentFile_ThrowsFileNotFoundException()
    {
        // Arrange
        string testFileName = "non-existent-file.json";

        // Act
        Action act = () => SeedHelper.SeedData<TestEntity>(testFileName);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void SeedData_WithInvalidJson_ThrowsJsonReaderException()
    {
        // Arrange
        string testFileName = "invalid-json.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(fullPath, "{this is not valid json");

        try
        {
            // Act
            Action act = () => SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            act.Should().Throw<JsonReaderException>();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithMismatchedJsonStructure_ThrowsJsonReaderException()
    {
        // Arrange
        string testFileName = "mismatched-json.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(fullPath, "[{\"Id\": \"not a number\", \"Name\": \"Test\"}]");

        try
        {
            // Act
            Action act = () => SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            act.Should().Throw<JsonReaderException>();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithSingleEntity_ReturnsSingleItemList()
    {
        // Arrange
        string testFileName = "single-entity.json";
        var expectedData = new List<TestEntity>
        {
            new TestEntity { Id = 42, Name = "SingleTest" }
        };
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        string json = JsonConvert.SerializeObject(expectedData);
        File.WriteAllText(fullPath, json);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result![0].Id.Should().Be(42);
            result[0].Name.Should().Be("SingleTest");
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithNullPropertyValues_DeserializesCorrectly()
    {
        // Arrange
        string testFileName = "null-properties.json";
        var expectedData = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = null }
        };
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        string json = JsonConvert.SerializeObject(expectedData);
        File.WriteAllText(fullPath, json);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result![0].Id.Should().Be(1);
            result[0].Name.Should().BeNull();
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    [Fact]
    public void SeedData_WithWhitespaceInJson_DeserializesCorrectly()
    {
        // Arrange
        string testFileName = "whitespace-json.json";
        string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models", "SeedData");
        Directory.CreateDirectory(testDirectory);
        string fullPath = Path.Combine(testDirectory, testFileName);
        string json = @"
        [
            {
                ""Id"": 1,
                ""Name"": ""Test1""
            },
            {
                ""Id"": 2,
                ""Name"": ""Test2""
            }
        ]";
        File.WriteAllText(fullPath, json);

        try
        {
            // Act
            var result = SeedHelper.SeedData<TestEntity>(testFileName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result![0].Id.Should().Be(1);
            result[0].Name.Should().Be("Test1");
        }
        finally
        {
            // Cleanup
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
