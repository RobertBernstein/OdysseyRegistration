using System.Reflection;
using FluentAssertions;
using JudgeRegistrationRazor.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Migrations;

public class UpdateConfigSeedDataTests
{
    [Fact]
    public void Up_ShouldCallUpdateData_SixTimes()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().HaveCount(6);
        invocations.Should().OnlyContain(inv => inv.Method.Name == "UpdateData");
    }

    [Fact]
    public void Up_ShouldUpdateData_InConfigTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[0].Equals("Config"));
    }

    [Fact]
    public void Up_ShouldUpdateData_WithKeyColumnId()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[1].Equals("Id"));
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectConfigIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().BeEquivalentTo([14, 15, 21, 22, 25, 26]);
    }

    [Fact]
    public void Up_ShouldUpdateData_WithColumnNameValue()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[3].Equals("Value"));
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId14()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId14Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 14);
        configId14Update.Should().NotBeNull();
        configId14Update!.Arguments[4].Should().Be("2025-02-29 23:59");
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId15()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId15Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 15);
        configId15Update.Should().NotBeNull();
        configId15Update!.Arguments[4].Should().Be("2024-09-28 00:00");
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId21()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId21Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 21);
        configId21Update.Should().NotBeNull();
        configId21Update!.Arguments[4].Should().Be("NoVA North");
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId22()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId22Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 22);
        configId22Update.Should().NotBeNull();
        configId22Update!.Arguments[4].Should().Be("9");
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId25()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId25Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 25);
        configId25Update.Should().NotBeNull();
        configId25Update!.Arguments[4].Should().Be("2025-01-25 23: 59");
    }

    [Fact]
    public void Up_ShouldUpdateData_WithCorrectValueForConfigId26()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId26Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 26);
        configId26Update.Should().NotBeNull();
        configId26Update!.Arguments[4].Should().Be("2024-09-15 00: 00");
    }

    [Fact]
    public void Up_ShouldUpdateData_InCorrectOrder()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().ContainInOrder(14, 15, 21, 22, 25, 26);
    }

    [Fact]
    public void Down_ShouldCallUpdateData_SixTimes()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().HaveCount(6);
        invocations.Should().OnlyContain(inv => inv.Method.Name == "UpdateData");
    }

    [Fact]
    public void Down_ShouldUpdateData_InConfigTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[0].Equals("Config"));
    }

    [Fact]
    public void Down_ShouldUpdateData_WithKeyColumnId()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[1].Equals("Id"));
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectConfigIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().BeEquivalentTo([14, 15, 21, 22, 25, 26]);
    }

    [Fact]
    public void Down_ShouldUpdateData_WithColumnNameValue()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        invocations.Should().OnlyContain(inv => inv.Arguments[3].Equals("Value"));
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId14()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId14Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 14);
        configId14Update.Should().NotBeNull();
        configId14Update!.Arguments[4].Should().Be("2024-02-29 23:59");
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId15()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId15Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 15);
        configId15Update.Should().NotBeNull();
        configId15Update!.Arguments[4].Should().Be("2023-11-28 00:00");
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId21()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId21Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 21);
        configId21Update.Should().NotBeNull();
        configId21Update!.Arguments[4].Should().Be("NoVA North and NoVA South");
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId22()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId22Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 22);
        configId22Update.Should().NotBeNull();
        configId22Update!.Arguments[4].Should().Be("9  & 12");
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId25()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId25Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 25);
        configId25Update.Should().NotBeNull();
        configId25Update!.Arguments[4].Should().Be("2024-1-25 23: 59");
    }

    [Fact]
    public void Down_ShouldUpdateData_WithCorrectValueForConfigId26()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var configId26Update = invocations.FirstOrDefault(inv => (int)inv.Arguments[2] == 26);
        configId26Update.Should().NotBeNull();
        configId26Update!.Arguments[4].Should().Be("2023-12-15 00: 00");
    }

    [Fact]
    public void Down_ShouldUpdateData_InCorrectOrder()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var keyValues = invocations.Select(inv => inv.Arguments[2]).Cast<int>().ToList();
        keyValues.Should().ContainInOrder(14, 15, 21, 22, 25, 26);
    }

    [Fact]
    public void Down_ShouldReverseUp_ForAllConfigIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new UpdateConfigSeedData();
        var upMethod = typeof(UpdateConfigSeedData).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);
        var downMethod = typeof(UpdateConfigSeedData).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act - Get Up config IDs
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);
        var upInvocations = mockMigrationBuilder.Invocations;
        var upConfigIds = upInvocations.Select(inv => (int)inv.Arguments[2]).ToList();

        // Reset the mock
        mockMigrationBuilder.Invocations.Clear();

        // Act - Get Down config IDs
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);
        var downInvocations = mockMigrationBuilder.Invocations;
        var downConfigIds = downInvocations.Select(inv => (int)inv.Arguments[2]).ToList();

        // Assert - Down should update the same config IDs as Up
        downConfigIds.Should().BeEquivalentTo(upConfigIds);
    }
}
