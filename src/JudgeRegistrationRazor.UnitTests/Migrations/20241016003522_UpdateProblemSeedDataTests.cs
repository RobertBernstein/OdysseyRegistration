using System.Reflection;
using FluentAssertions;
using JudgeRegistrationRazor.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Migrations;

public class UpdateProblemSeedDataTests
{
    [Fact]
    public void Up_ShouldCallInsertData_WithProblemTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().ContainSingle();
        invocations[0].Method.Name.Should().Be("InsertData");
        invocations[0].Arguments[0].Should().Be("Problem");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectColumns()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        columns.Should().NotBeNull();
        columns.Should().Contain("Id");
        columns.Should().Contain("CostLimit");
        columns.Should().Contain("Divisions");
        columns.Should().Contain("Notes");
        columns.Should().Contain("PCAddress");
        columns.Should().Contain("PCCity");
        columns.Should().Contain("PCEmail1");
        columns.Should().Contain("PCEmail2");
        columns.Should().Contain("ProblemName");
        columns.Should().Contain("ProblemCategory");
        columns.Should().Contain("ProblemDescription");
        columns.Should().Contain("ProblemCaptainID");
    }

    [Fact]
    public void Up_ShouldInsertData_WithEightRows()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var values = invocations[0].Arguments[2];
        values.Should().NotBeNull();
        
        // The values should be a multidimensional array with 8 rows
        if (values is Array arr)
        {
            arr.Rank.Should().Be(2);
            arr.GetLength(0).Should().Be(8); // 8 rows
        }
        else
        {
            // If it's a flat array, calculate row count
            var columns = invocations[0].Arguments[1] as string[];
            var valuesArray = values as object?[];
            var columnCount = columns?.Length ?? 0;
            var rowCount = valuesArray!.Length / columnCount;
            rowCount.Should().Be(8);
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectProblemIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var idIndex = Array.IndexOf(columns!, "Id");
        
        // Extract all IDs (first column of each row)
        var ids = new List<int>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                ids.Add((int)multiArray[row, idIndex]!);
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                ids.Add((int)flatArray[i + idIndex]!);
            }
        }
        
        ids.Should().BeEquivalentTo([1, 2, 3, 4, 5, 6, 7, 8]);
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectProblemNames()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var nameIndex = Array.IndexOf(columns!, "ProblemName");
        
        // Extract all problem names
        var names = new List<string>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                names.Add((string)multiArray[row, nameIndex]!);
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                names.Add((string)flatArray[i + nameIndex]!);
            }
        }
        
        names.Should().Contain("I Don't Know");
        names.Should().Contain("P1:Drive-in Movie");
        names.Should().Contain("P2:AI Tech-No-Art");
        names.Should().Contain("P3: Classics... Opening Night Antics");
        names.Should().Contain("P4: Deep Space Structure");
        names.Should().Contain("P5:Rocking World Detour");
        names.Should().Contain("Dinos on Parade");
        names.Should().Contain("Spontaneous");
    }

    [Fact]
    public void Down_ShouldCallDeleteData_EightTimes()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().HaveCount(8);
        invocations.Should().OnlyContain(inv => inv.Method.Name == "DeleteData");
    }

    [Fact]
    public void Down_ShouldDeleteData_FromProblemTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[0].Equals("Problem"));
    }

    [Fact]
    public void Down_ShouldDeleteData_WithKeyColumnId()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[1].Equals("Id"));
    }

    [Fact]
    public void Down_ShouldDeleteData_WithCorrectKeyValues()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().BeEquivalentTo([1, 2, 3, 4, 5, 6, 7, 8]);
    }

    [Fact]
    public void Down_ShouldDeleteData_InCorrectOrder()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().ContainInOrder(1, 2, 3, 4, 5, 6, 7, 8);
    }

    [Fact]
    public void Up_ShouldInsertData_WithNullSchema()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations[0].Arguments[3].Should().BeNull();
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectCostLimits()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var costLimitIndex = Array.IndexOf(columns!, "CostLimit");
        
        if (values is object?[,] multiArray)
        {
            multiArray[0, costLimitIndex].Should().BeNull(); // I Don't Know
            multiArray[1, costLimitIndex].Should().Be("$145 USD"); // P1
            multiArray[2, costLimitIndex].Should().Be("$145 USD"); // P2
            multiArray[3, costLimitIndex].Should().Be("$125 USD"); // P3
            multiArray[4, costLimitIndex].Should().Be("$145 USD"); // P4
            multiArray[5, costLimitIndex].Should().Be("$125 USD"); // P5
            multiArray[6, costLimitIndex].Should().Be("$125 USD"); // Primary
            multiArray[7, costLimitIndex].Should().Be("N/A"); // Spontaneous
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectProblemCategories()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var categoryIndex = Array.IndexOf(columns!, "ProblemCategory");
        
        if (values is object?[,] multiArray)
        {
            multiArray[0, categoryIndex].Should().BeNull(); // I Don't Know
            multiArray[1, categoryIndex].Should().Be("Vehicle"); // P1
            multiArray[2, categoryIndex].Should().Be("Technical"); // P2
            multiArray[3, categoryIndex].Should().Be("Classics"); // P3
            multiArray[4, categoryIndex].Should().Be("Structure"); // P4
            multiArray[5, categoryIndex].Should().Be("Drama"); // P5
            multiArray[6, categoryIndex].Should().Be("Primary"); // Primary
            multiArray[7, categoryIndex].Should().BeNull(); // Spontaneous
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectDivisions()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var divisionsIndex = Array.IndexOf(columns!, "Divisions");
        
        if (values is object?[,] multiArray)
        {
            multiArray[0, divisionsIndex].Should().BeNull(); // I Don't Know
            multiArray[1, divisionsIndex].Should().Be("I, II, III, & IV"); // P1
            multiArray[2, divisionsIndex].Should().Be("I, II, & III"); // P2
            multiArray[3, divisionsIndex].Should().Be("I, II, III & IV"); // P3
            multiArray[6, divisionsIndex].Should().Be("Grades K-2"); // Primary
            multiArray[7, divisionsIndex].Should().Be("All"); // Spontaneous
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectProblemCaptainIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var captainIdIndex = Array.IndexOf(columns!, "ProblemCaptainID");
        
        if (values is object?[,] multiArray)
        {
            multiArray[0, captainIdIndex].Should().BeNull(); // I Don't Know
            multiArray[1, captainIdIndex].Should().Be("1"); // P1
            multiArray[2, captainIdIndex].Should().Be("2"); // P2
            multiArray[3, captainIdIndex].Should().Be("3"); // P3
            multiArray[4, captainIdIndex].Should().Be("4"); // P4
            multiArray[5, captainIdIndex].Should().Be("5"); // P5
            multiArray[6, captainIdIndex].Should().Be("6"); // Primary
            multiArray[7, captainIdIndex].Should().Be("7"); // Spontaneous
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithSpontaneousProblemDescription()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var descriptionIndex = Array.IndexOf(columns!, "ProblemDescription");
        
        if (values is object?[,] multiArray)
        {
            var spontaneousDescription = multiArray[7, descriptionIndex] as string;
            spontaneousDescription.Should().Contain("No synopsis");
            spontaneousDescription.Should().Contain("presented with a problem they have never seen before");
            spontaneousDescription.Should().Contain("solved on-the-spot");
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithEmptyStringsForContactInfo()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var upMethod = typeof(UpdateProblemSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var emailIndex = Array.IndexOf(columns!, "PCEmail1");
        var addressIndex = Array.IndexOf(columns!, "PCAddress");
        
        if (values is object?[,] multiArray)
        {
            // Row 2 (P1) should have empty strings, not null
            multiArray[1, emailIndex].Should().Be("");
            multiArray[1, addressIndex].Should().Be("");
        }
    }

    [Fact]
    public void Down_ShouldDeleteData_WithNullSchema()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateProblemSeedData();
        var downMethod = typeof(UpdateProblemSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => 
            inv.Arguments.Count > 3 ? inv.Arguments[3] == null : true);
    }
}
