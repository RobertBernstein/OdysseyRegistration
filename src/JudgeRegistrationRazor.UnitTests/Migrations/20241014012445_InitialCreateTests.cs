using System.Reflection;
using FluentAssertions;
using JudgeRegistrationRazor.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Migrations;

public class InitialCreateTests
{
    [Fact]
    public void Up_ShouldCreateTable_CoachesTrainingDivisions()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var createTableInvocations = invocations.Where(inv => inv.Method.Name == "CreateTable").ToList();
        createTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("CoachesTrainingDivisions"));
    }

    [Fact]
    public void Up_ShouldCreate13Tables()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var createTableInvocations = invocations.Where(inv => inv.Method.Name == "CreateTable").ToList();
        createTableInvocations.Should().HaveCount(13);
    }

    [Fact]
    public void Up_ShouldCreateAllExpectedTables()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var createTableInvocations = invocations.Where(inv => inv.Method.Name == "CreateTable").ToList();
        var tableNames = createTableInvocations.Select(inv => inv.Arguments[0] as string).ToList();

        tableNames.Should().Contain("CoachesTrainingDivisions");
        tableNames.Should().Contain("CoachesTrainingRegions");
        tableNames.Should().Contain("CoachesTrainingRegistrations");
        tableNames.Should().Contain("CoachesTrainingRoles");
        tableNames.Should().Contain("Config");
        tableNames.Should().Contain("ContactUsRecipients");
        tableNames.Should().Contain("ContactUsSenderRoles");
        tableNames.Should().Contain("Events");
        tableNames.Should().Contain("Judges");
        tableNames.Should().Contain("Problem");
        tableNames.Should().Contain("Schools");
        tableNames.Should().Contain("TournamentRegistration");
        tableNames.Should().Contain("Volunteers");
    }

    [Fact]
    public void Up_ShouldInsertData_IntoConfigTable()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        insertDataInvocations.Should().ContainSingle();
        insertDataInvocations[0].Arguments[0].Should().Be("Config");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectConfigColumns()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];

        columns.Should().NotBeNull();
        columns.Should().Contain("Id");
        columns.Should().Contain("Name");
        columns.Should().Contain("Value");
        columns.Should().HaveCount(3);
    }

    [Fact]
    public void Up_ShouldInsertData_With34ConfigRows()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var values = insertDataInvocations[0].Arguments[2];

        values.Should().NotBeNull();

        if (values is Array arr)
        {
            arr.Rank.Should().Be(2);
            arr.GetLength(0).Should().Be(34);
        }
        else
        {
            var columns = insertDataInvocations[0].Arguments[1] as string[];
            var valuesArray = values as object?[];
            var columnCount = columns?.Length ?? 0;
            var rowCount = valuesArray!.Length / columnCount;
            rowCount.Should().Be(34);
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectConfigNames()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var nameIndex = Array.IndexOf(columns!, "Name");

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

        names.Should().Contain("AcceptingPayPal");
        names.Should().Contain("CoachesHandbookURL");
        names.Should().Contain("EmailServer");
        names.Should().Contain("HomePage");
        names.Should().Contain("RegionName");
        names.Should().Contain("RegionNumber");
        names.Should().Contain("Year");
        names.Should().Contain("WebmasterEmail");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectConfigValues()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var nameIndex = Array.IndexOf(columns!, "Name");
        var valueIndex = Array.IndexOf(columns!, "Value");

        var configData = new Dictionary<string, string?>();
        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                var name = multiArray[row, nameIndex] as string;
                var value = multiArray[row, valueIndex] as string;
                if (name != null)
                {
                    configData[name] = value;
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                var name = flatArray[i + nameIndex] as string;
                var value = flatArray[i + valueIndex] as string;
                if (name != null)
                {
                    configData[name] = value;
                }
            }
        }

        configData["AcceptingPayPal"].Should().Be("True");
        configData["HomePage"].Should().Be("https://www.novanorth.org");
        configData["RegionName"].Should().Be("NoVA North and NoVA South");
        configData["RegionNumber"].Should().Be("9  & 12");
        configData["Year"].Should().Be("2023");
        configData["EmailServer"].Should().Be("mail.novanorth.org");
        configData["WebmasterEmail"].Should().Be("webmaster@novanorth.org");
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectFirstConfigRow()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var idIndex = Array.IndexOf(columns!, "Id");
        var nameIndex = Array.IndexOf(columns!, "Name");
        var valueIndex = Array.IndexOf(columns!, "Value");

        if (values is object?[,] multiArray)
        {
            multiArray[0, idIndex].Should().Be(1);
            multiArray[0, nameIndex].Should().Be("AcceptingPayPal");
            multiArray[0, valueIndex].Should().Be("True");
        }
        else if (values is object?[] flatArray)
        {
            flatArray[idIndex].Should().Be(1);
            flatArray[nameIndex].Should().Be("AcceptingPayPal");
            flatArray[valueIndex].Should().Be("True");
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithCorrectLastConfigRow()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var idIndex = Array.IndexOf(columns!, "Id");
        var nameIndex = Array.IndexOf(columns!, "Name");
        var valueIndex = Array.IndexOf(columns!, "Value");

        if (values is object?[,] multiArray)
        {
            var lastRow = multiArray.GetLength(0) - 1;
            multiArray[lastRow, idIndex].Should().Be(34);
            multiArray[lastRow, nameIndex].Should().Be("Year");
            multiArray[lastRow, valueIndex].Should().Be("2023");
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            var lastIndex = flatArray.Length - columnCount;
            flatArray[lastIndex + idIndex].Should().Be(34);
            flatArray[lastIndex + nameIndex].Should().Be("Year");
            flatArray[lastIndex + valueIndex].Should().Be("2023");
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithSequentialConfigIds()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var idIndex = Array.IndexOf(columns!, "Id");

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

        ids.Should().BeInAscendingOrder();
        ids.Should().StartWith(1);
        ids.Should().EndWith(34);
        ids.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void Up_ShouldInsertData_WithEmptyStringForCoachesHandbookURL()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var nameIndex = Array.IndexOf(columns!, "Name");
        var valueIndex = Array.IndexOf(columns!, "Value");

        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if (multiArray[row, nameIndex] as string == "CoachesHandbookURL")
                {
                    multiArray[row, valueIndex].Should().Be("");
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                if (flatArray[i + nameIndex] as string == "CoachesHandbookURL")
                {
                    flatArray[i + valueIndex].Should().Be("");
                }
            }
        }
    }

    [Fact]
    public void Up_ShouldInsertData_WithEmptyStringForWebmasterEmailPassword()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var insertDataInvocations = invocations.Where(inv => inv.Method.Name == "InsertData").ToList();
        var columns = insertDataInvocations[0].Arguments[1] as string[];
        var values = insertDataInvocations[0].Arguments[2];

        var nameIndex = Array.IndexOf(columns!, "Name");
        var valueIndex = Array.IndexOf(columns!, "Value");

        if (values is object?[,] multiArray)
        {
            for (var row = 0; row < multiArray.GetLength(0); row++)
            {
                if (multiArray[row, nameIndex] as string == "WebmasterEmailPassword")
                {
                    multiArray[row, valueIndex].Should().Be("");
                }
            }
        }
        else if (values is object?[] flatArray)
        {
            var columnCount = columns!.Length;
            for (var i = 0; i < flatArray.Length; i += columnCount)
            {
                if (flatArray[i + nameIndex] as string == "WebmasterEmailPassword")
                {
                    flatArray[i + valueIndex].Should().Be("");
                }
            }
        }
    }

    [Fact]
    public void Down_ShouldDropTable_CoachesTrainingDivisions()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("CoachesTrainingDivisions"));
    }

    [Fact]
    public void Down_ShouldDrop13Tables()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().HaveCount(13);
    }

    [Fact]
    public void Down_ShouldDropAllTables()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        var tableNames = dropTableInvocations.Select(inv => inv.Arguments[0] as string).ToList();

        tableNames.Should().Contain("CoachesTrainingDivisions");
        tableNames.Should().Contain("CoachesTrainingRegions");
        tableNames.Should().Contain("CoachesTrainingRegistrations");
        tableNames.Should().Contain("CoachesTrainingRoles");
        tableNames.Should().Contain("Config");
        tableNames.Should().Contain("ContactUsRecipients");
        tableNames.Should().Contain("ContactUsSenderRoles");
        tableNames.Should().Contain("Events");
        tableNames.Should().Contain("Judges");
        tableNames.Should().Contain("Problem");
        tableNames.Should().Contain("Schools");
        tableNames.Should().Contain("TournamentRegistration");
        tableNames.Should().Contain("Volunteers");
    }

    [Fact]
    public void Down_ShouldDropTable_Config()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Config"));
    }

    [Fact]
    public void Down_ShouldDropTable_Judges()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Judges"));
    }

    [Fact]
    public void Down_ShouldDropTable_TournamentRegistration()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("TournamentRegistration"));
    }

    [Fact]
    public void Down_ShouldDropTable_Events()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Events"));
    }

    [Fact]
    public void Down_ShouldDropTable_Volunteers()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Volunteers"));
    }

    [Fact]
    public void Down_ShouldDropTable_Schools()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Schools"));
    }

    [Fact]
    public void Down_ShouldDropTable_Problem()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("Problem"));
    }

    [Fact]
    public void Down_ShouldDropTable_ContactUsRecipients()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("ContactUsRecipients"));
    }

    [Fact]
    public void Down_ShouldDropTable_ContactUsSenderRoles()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("ContactUsSenderRoles"));
    }

    [Fact]
    public void Down_ShouldDropTable_CoachesTrainingRegions()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("CoachesTrainingRegions"));
    }

    [Fact]
    public void Down_ShouldDropTable_CoachesTrainingRegistrations()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("CoachesTrainingRegistrations"));
    }

    [Fact]
    public void Down_ShouldDropTable_CoachesTrainingRoles()
    {
        // Arrange
        var mockMigrationBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        downMethod?.Invoke(migration, [mockMigrationBuilder.Object]);

        // Assert
        var invocations = mockMigrationBuilder.Invocations;
        var dropTableInvocations = invocations.Where(inv => inv.Method.Name == "DropTable").ToList();
        dropTableInvocations.Should().Contain(inv => inv.Arguments[0].Equals("CoachesTrainingRoles"));
    }

    [Fact]
    public void Up_AndThen_Down_ShouldCreateAndDropSameTables()
    {
        // Arrange
        var mockUpBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var mockDownBuilder = new Mock<MigrationBuilder>("Microsoft.EntityFrameworkCore.SqlServer");
        var migration = new InitialCreate();
        var upMethod = typeof(InitialCreate).GetMethod("Up", BindingFlags.NonPublic | BindingFlags.Instance);
        var downMethod = typeof(InitialCreate).GetMethod("Down", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        upMethod?.Invoke(migration, [mockUpBuilder.Object]);
        downMethod?.Invoke(migration, [mockDownBuilder.Object]);

        // Assert
        var createdTables = mockUpBuilder.Invocations
            .Where(inv => inv.Method.Name == "CreateTable")
            .Select(inv => inv.Arguments[0] as string)
            .ToList();

        var droppedTables = mockDownBuilder.Invocations
            .Where(inv => inv.Method.Name == "DropTable")
            .Select(inv => inv.Arguments[0] as string)
            .ToList();

        droppedTables.Should().BeEquivalentTo(createdTables);
    }
}
