using System.Reflection;
using FluentAssertions;
using JudgeRegistrationRazor.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Migrations;

public class UpdateEventsSeedDataTests
{
    [Fact]
    public void Up_ShouldCallInsertData_WithEventsTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().ContainSingle();
        invocations[0].Method.Name.Should().Be("InsertData");
        invocations[0].Arguments[0].Should().Be("Events");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectColumns()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        columns.Should().NotBeNull();
        columns.Should().Contain("Id");
        columns.Should().Contain("EndDate");
        columns.Should().Contain("EventCoordinatorEmail");
        columns.Should().Contain("EventCoordinatorName");
        columns.Should().Contain("EventCoordinatorPhone");
        columns.Should().Contain("EventCost");
        columns.Should().Contain("EventMailBody");
        columns.Should().Contain("EventMakeChecksOutTo");
        columns.Should().Contain("EventName");
        columns.Should().Contain("EventPayeeAddress1");
        columns.Should().Contain("EventPayeeAddress2");
        columns.Should().Contain("EventPayeeCity");
        columns.Should().Contain("EventPayeeEmail1");
        columns.Should().Contain("EventPayeeName");
        columns.Should().Contain("EventPayeePhone1");
        columns.Should().Contain("EventPayeeState");
        columns.Should().Contain("EventPayeeZipCode");
        columns.Should().Contain("EventVolunteerInformationMessage");
        columns.Should().Contain("InformationURL");
        columns.Should().Contain("LateEventCost");
        columns.Should().Contain("LateEventCostStartDate");
        columns.Should().Contain("Location");
        columns.Should().Contain("LocationAddress");
        columns.Should().Contain("LocationCity");
        columns.Should().Contain("LocationMapURL");
        columns.Should().Contain("LocationPhone");
        columns.Should().Contain("LocationState");
        columns.Should().Contain("LocationURL");
        columns.Should().Contain("LocationURLColor");
        columns.Should().Contain("PaymentDueDate");
        columns.Should().Contain("StartDate");
        columns.Should().Contain("TeamsVolunteerWantsToSeeMessage");
        columns.Should().Contain("Time");
    }

    [Fact]
    public void Up_ShouldInsertData_WithTenRows()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var values = invocations[0].Arguments[2];
        values.Should().NotBeNull();
        
        // The values should be a multidimensional array with 10 rows
        if (values is Array arr)
        {
            arr.Rank.Should().Be(2);
            arr.GetLength(0).Should().Be(10); // 10 rows
        }
        else
        {
            // If it's a flat array, calculate row count
            var columns = invocations[0].Arguments[1] as string[];
            var valuesArray = values as object?[];
            var columnCount = columns?.Length ?? 0;
            var rowCount = valuesArray!.Length / columnCount;
            rowCount.Should().Be(10);
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectEventIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

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
        
        ids.Should().BeEquivalentTo([10, 20, 30, 60, 70, 90, 95, 100, 110, 120]);
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectEventNames()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var nameIndex = Array.IndexOf(columns!, "EventName");
        
        // Extract all event names
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
        
        names.Should().Contain("Coordinators Meeting");
        names.Should().Contain("Coordinators Meeting #2");
        names.Should().Contain("Coaches' Training");
        names.Should().Contain("Regional Tournament Team Registration Deadline");
        names.Should().Contain("Coaches Q&A");
        names.Should().Contain("Judges Training");
        names.Should().Contain("Volunteer Registration");
        names.Should().Contain("NoVA North Regional Tournament");
        names.Should().Contain("Virginia State Tournament");
        names.Should().Contain("Odyssey of the Mind World Finals");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectLocationForEvent10()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var idIndex = Array.IndexOf(columns!, "Id");
        var locationIndex = Array.IndexOf(columns!, "Location");
        
        // Find event with ID 10
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if ((int)multiArray[row, idIndex]! == 10)
                {
                    multiArray[row, locationIndex].Should().Be("Dolley Madison Library");
                    return;
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                if ((int)flatArray[i + idIndex]! == 10)
                {
                    flatArray[i + locationIndex].Should().Be("Dolley Madison Library");
                    return;
                }
            }
        }
        
        Assert.Fail("Event with ID 10 not found");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectEventCostForEvent100()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var idIndex = Array.IndexOf(columns!, "Id");
        var costIndex = Array.IndexOf(columns!, "EventCost");
        
        // Find event with ID 100
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if ((int)multiArray[row, idIndex]! == 100)
                {
                    multiArray[row, costIndex].Should().Be("90");
                    return;
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
                    flatArray[i + costIndex].Should().Be("90");
                    return;
                }
            }
        }
        
        Assert.Fail("Event with ID 100 not found");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectStartDateForEvent70()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var columns = invocations[0].Arguments[1] as string[];
        var values = invocations[0].Arguments[2];
        
        var idIndex = Array.IndexOf(columns!, "Id");
        var startDateIndex = Array.IndexOf(columns!, "StartDate");
        
        // Find event with ID 70
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if ((int)multiArray[row, idIndex]! == 70)
                {
                    multiArray[row, startDateIndex].Should().Be(new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));
                    return;
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                if ((int)flatArray[i + idIndex]! == 70)
                {
                    flatArray[i + startDateIndex].Should().Be(new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));
                    return;
                }
            }
        }
        
        Assert.Fail("Event with ID 70 not found");
    }

    [Fact]
    public void Up_ShouldInsertData_WithNullSchema()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        mockMigrationBuilder.Verify(
            m => m.InsertData(
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<object?[,]>(),
                null),
            Times.Once);
    }

    [Fact]
    public void Down_ShouldCallDeleteData_TenTimes()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().HaveCount(10);
        invocations.Should().OnlyContain(inv => inv.Method.Name == "DeleteData");
    }

    [Fact]
    public void Down_ShouldDeleteData_FromEventsTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[0].Equals("Events"));
    }

    [Fact]
    public void Down_ShouldDeleteData_WithKeyColumnId()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

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
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().BeEquivalentTo([10, 20, 30, 60, 70, 90, 95, 100, 110, 120]);
    }

    [Fact]
    public void Down_ShouldDeleteData_InCorrectOrder()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().ContainInOrder(10, 20, 30, 60, 70, 90, 95, 100, 110, 120);
    }

    [Fact]
    public void Down_ShouldDeleteData_WithNullSchema()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        mockMigrationBuilder.Verify(
            m => m.DeleteData(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                null),
            Times.Exactly(10));
    }

    [Fact]
    public void Down_ShouldDeleteData_IncludingAllInsertedIds()
    {
        // Arrange
        var mockUpBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var mockDownBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var upMethod = typeof(UpdateEventsSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockUpBuilder.Object]);
        downMethod?.Invoke(migration, [mockDownBuilder.Object]);

        // Assert
        var upInvocations = mockUpBuilder.Invocations;
        var downInvocations = mockDownBuilder.Invocations;
        
        // Extract IDs from Up's InsertData
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
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                insertedIds.Add((int)flatArray[i + idIndex]!);
            }
        }
        
        // Extract IDs from Down's DeleteData
        var deletedIds = downInvocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        
        // All inserted IDs should be deleted
        deletedIds.Should().BeEquivalentTo(insertedIds);
    }

    [Fact]
    public void Down_ShouldDeleteData_ForSpecificEventIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateEventsSeedData();
        var downMethod = typeof(UpdateEventsSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        
        // Verify specific IDs are present
        keyValues.Should().Contain(10); // Coordinators Meeting
        keyValues.Should().Contain(20); // Coordinators Meeting #2
        keyValues.Should().Contain(30); // Coaches' Training
        keyValues.Should().Contain(60); // Regional Tournament Team Registration Deadline
        keyValues.Should().Contain(70); // Coaches Q&A
        keyValues.Should().Contain(90); // Judges Training
        keyValues.Should().Contain(95); // Volunteer Registration
        keyValues.Should().Contain(100); // NoVA North Regional Tournament
        keyValues.Should().Contain(110); // Virginia State Tournament
        keyValues.Should().Contain(120); // Odyssey of the Mind World Finals
    }
}
