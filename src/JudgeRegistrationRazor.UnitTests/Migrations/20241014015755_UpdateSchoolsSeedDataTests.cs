using System.Reflection;
using FluentAssertions;
using JudgeRegistrationRazor.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Migrations;

public class UpdateSchoolsSeedDataTests
{
    [Fact]
    public void Up_ShouldCallInsertData_WithSchoolsTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().ContainSingle();
        invocations[0].Method.Name.Should().Be("InsertData");
        invocations[0].Arguments[0].Should().Be("Schools");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectColumns()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        columns.Should().NotBeNull();
        columns.Should().Contain("Id");
        columns.Should().Contain("Address");
        columns.Should().Contain("City");
        columns.Should().Contain("CoordAddress");
        columns.Should().Contain("CoordAltPhone");
        columns.Should().Contain("CoordCity");
        columns.Should().Contain("CoordEmailName");
        columns.Should().Contain("CoordFaxNumber");
        columns.Should().Contain("CoordFirstName");
        columns.Should().Contain("CoordLastName");
        columns.Should().Contain("CoordMobilePhone");
        columns.Should().Contain("CoordNew?");
        columns.Should().Contain("CoordPhone");
        columns.Should().Contain("CoordPostalCode");
        columns.Should().Contain("CoordState");
        columns.Should().Contain("Membership#1");
        columns.Should().Contain("Membership#1seen");
        columns.Should().Contain("Membership#2");
        columns.Should().Contain("Membership#2seen");
        columns.Should().Contain("Membership#3");
        columns.Should().Contain("Membership#3seen");
        columns.Should().Contain("Membership#4");
        columns.Should().Contain("Membership#4seen");
        columns.Should().Contain("Name");
        columns.Should().Contain("Notes");
        columns.Should().Contain("Phone");
        columns.Should().Contain("PostalCode");
        columns.Should().Contain("RegionNumber");
        columns.Should().Contain("Share?");
        columns.Should().Contain("State");
    }

    [Fact]
    public void Up_ShouldInsertData_With112Rows()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var values = invocations[0].Arguments[2];
        values.Should().NotBeNull();
        
        // The values should be a multidimensional array with 112 rows
        if (values is Array arr)
        {
            arr.Rank.Should().Be(2);
            arr.GetLength(0).Should().Be(112); // 112 rows
        }
        else
        {
            // If it's a flat array, calculate row count
            var columns = invocations[0].Arguments[1] as string[];
            var valuesArray = values as object?[];
            var columnCount = columns?.Length ?? 0;
            var rowCount = valuesArray!.Length / columnCount;
            rowCount.Should().Be(112);
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectSchoolIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

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
        
        ids.Should().Contain([8, 10, 11, 16, 19, 21, 25, 28, 29, 35, 42, 44, 46, 69, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 83, 86, 89, 90, 94, 99, 100, 223]);
        ids.Should().HaveCount(112);
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectSchoolNames()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var nameIndex = Array.IndexOf(columns!, "Name");
        
        // Extract all school names
        var names = new List<string?>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                names.Add(multiArray[row, nameIndex] as string);
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                names.Add(flatArray[i + nameIndex] as string);
            }
        }
        
        names.Should().Contain("Crossfield Elementary (27938)");
        names.Should().Contain("Forest Edge Elementary (30388)");
        names.Should().Contain("Fox Mill Elementary (28383)");
        names.Should().Contain("Oakton High (32076)");
        names.Should().Contain("Thomas Jefferson High Sci/Tech (5808)");
        names.Should().Contain("Powell Elementary School (39730)");
    }

    [Fact]
    public void Up_ShouldInsertData_WithRegionNumbers()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var regionIndex = Array.IndexOf(columns!, "RegionNumber");
        
        // Extract all region numbers
        var regions = new List<short>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                regions.Add((short)multiArray[row, regionIndex]!);
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                regions.Add((short)flatArray[i + regionIndex]!);
            }
        }
        
        // Most schools should be in region 9, some in region 11 and 12
        regions.Should().Contain(9);
        regions.Should().Contain(11);
        regions.Should().Contain(12);
        regions.Where(r => r == 9).Should().HaveCountGreaterThan(80); // Majority are in region 9
    }

    [Fact]
    public void Up_ShouldInsertData_WithNullableColumnsAsNull()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var addressIndex = Array.IndexOf(columns!, "Address");
        
        // Most schools have null addresses in this seed data
        if (values is object?[,] multiArray)
        {
            multiArray[0, addressIndex].Should().BeNull();
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            flatArray[addressIndex].Should().BeNull();
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithNullNameForId100()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var idIndex = Array.IndexOf(columns!, "Id");
        var nameIndex = Array.IndexOf(columns!, "Name");
        
        // Find the row with Id=100
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if ((int)multiArray[row, idIndex]! == 100)
                {
                    multiArray[row, nameIndex].Should().BeNull();
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                if ((int)flatArray[i + idIndex]! == 100)
                {
                    flatArray[i + nameIndex].Should().BeNull();
                }
            }
        }
    }

    [Fact]
    public void Down_ShouldCallDeleteData_112Times()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().HaveCount(112);
        invocations.Should().OnlyContain(inv => inv.Method.Name == "DeleteData");
    }

    [Fact]
    public void Down_ShouldDeleteData_FromSchoolsTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[0].Equals("Schools"));
    }

    [Fact]
    public void Down_ShouldDeleteData_WithKeyColumnId()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

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
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().Contain([8, 10, 11, 16, 19, 21, 25, 28, 29, 35, 42, 44, 46, 69, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 83, 86, 89, 90, 94, 99, 100, 223]);
        keyValues.Should().HaveCount(112);
    }

    [Fact]
    public void Down_ShouldDeleteData_InCorrectOrder()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues[0].Should().Be(8);
        keyValues[1].Should().Be(10);
        keyValues[2].Should().Be(11);
        keyValues[111].Should().Be(223);
    }

    [Fact]
    public void Down_ShouldDeleteData_IncludingAllInsertedIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var upMethod = typeof(UpdateSchoolsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act - First get the IDs from Up
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);
        var upInvocations = mockMigrationBuilder.Invocations;
        var columns = upInvocations[0].Arguments[1] as string[];
        var values = upInvocations[0].Arguments[2];
        var idIndex = Array.IndexOf(columns!, "Id");
        
        var insertedIds = new List<int>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                insertedIds.Add((int)multiArray[row, idIndex]!);
            }
        }

        // Reset the mock
        mockMigrationBuilder.Invocations.Clear();

        // Act - Then get the IDs from Down
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);
        var downInvocations = mockMigrationBuilder.Invocations;
        var deletedIds = downInvocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();

        // Assert - All inserted IDs should be deleted
        deletedIds.Should().BeEquivalentTo(insertedIds);
    }

    [Fact]
    public void Down_ShouldDeleteData_ForSpecificSchoolIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateSchoolsSeedData();
        var downMethod = typeof(UpdateSchoolsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        
        // Verify some specific IDs from different regions
        keyValues.Should().Contain(163); // Thomas Jefferson High Sci/Tech (Region 11)
        keyValues.Should().Contain(153); // Canterbury Woods Ele Sch (Region 12)
        keyValues.Should().Contain(208); // Immanuel Christian School (Region 11)
    }
}
