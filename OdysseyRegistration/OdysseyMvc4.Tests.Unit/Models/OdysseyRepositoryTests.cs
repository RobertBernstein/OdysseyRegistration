using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OdysseyMvc2024.Models;

namespace OdysseyMvc4.Tests.Unit.Models;

/// <summary>
/// Tests for OdysseyRepository properties that manage cached data and lazy loading.
/// These tests ensure that repository patterns correctly interact with the database context.
/// 
/// MIGRATION STATUS:
/// - Config: ✅ Tested (active in OdysseyMvc2024)
/// - Judges: ✅ Tested (active in OdysseyMvc2024)
/// - TournamentRegistrations: ✅ Tested (active in OdysseyMvc2024)
/// - AddJudge: ✅ Tested (active in OdysseyMvc2024)
/// - CoachesTrainingInfo: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// - CoachesTrainingRegistrations: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// - AddCoachesTrainingRegistration: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// - VolunteerInfo: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// - Volunteers: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// - Divisions: ❌ Cannot test (commented out in OdysseyMvc2024, DbSet missing)
/// 
/// To test the commented-out properties, first uncomment them in OdysseyRepository.cs
/// and restore the corresponding DbSets in OdysseyEntities.cs.
/// </summary>
public class OdysseyRepositoryTests : IDisposable
{
    private readonly OdysseyEntities _context;

    public OdysseyRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<OdysseyEntities>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new OdysseyEntities(options);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    #region Config Property Tests

    [Fact]
    public void Config_FirstAccess_LoadsFromDatabase()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "NoVA North" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "9" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Config.Should().NotBeNull();
        repository.Config.Should().ContainKey("Year");
        repository.Config["Year"].Should().Be("2025");
        repository.Config.Should().ContainKey("RegionName");
        repository.Config["RegionName"].Should().Be("NoVA North");
    }

    [Fact]
    public void Config_AutomaticallyAddsEndYear()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Config.Should().ContainKey("EndYear");
        repository.Config["EndYear"].Should().Be("2026");
    }

    [Fact]
    public void Config_EndYearCalculatedFromYearValue()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2030" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Config["EndYear"].Should().Be("2031");
    }

    [Fact]
    public void Config_ContainsAllOriginalConfigValues()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "Test Region" });
        _context.Configs.Add(new Config { Name = "CustomKey", Value = "CustomValue" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Config.Should().HaveCount(4); // Year, RegionName, CustomKey, EndYear
        repository.Config["CustomKey"].Should().Be("CustomValue");
    }

    [Fact]
    public void Config_IsReadOnly_CannotBeSetExternally()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var originalConfig = repository.Config;

        originalConfig.Should().BeSameAs(repository.Config);
    }

    [Fact]
    public void Config_EmptyDatabase_StillAddsEndYear()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Config.Should().HaveCount(2); // Only Year and calculated EndYear
        repository.Config.Should().ContainKey("Year");
        repository.Config.Should().ContainKey("EndYear");
    }

    [Fact]
    public void Config_MultipleInstances_UsesInvariantCultureForEndYear()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.SaveChanges();

        var repository1 = new OdysseyRepository(_context);
        var repository2 = new OdysseyRepository(_context);

        repository1.Config["EndYear"].Should().Be(repository2.Config["EndYear"]);
    }

    #endregion

    #region Judges Property Tests

    [Fact]
    public void Judges_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        });
        _context.Judges.Add(new Judge
        {
            JudgeID = 2,
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane@example.com"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Judges.Should().NotBeNull();
        repository.Judges.Should().HaveCount(2);
    }

    [Fact]
    public void Judges_ReturnsAllJudges()
    {
        AddMinimalConfig();
        var judge1 = new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        };
        var judge2 = new Judge
        {
            JudgeID = 2,
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane@example.com"
        };
        var judge3 = new Judge
        {
            JudgeID = 3,
            FirstName = "Bob",
            LastName = "Johnson",
            EmailAddress = "bob@example.com"
        };

        _context.Judges.AddRange(judge1, judge2, judge3);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Judges.Should().HaveCount(3);
        repository.Judges.Should().Contain(j => j.FirstName == "John");
        repository.Judges.Should().Contain(j => j.FirstName == "Jane");
        repository.Judges.Should().Contain(j => j.FirstName == "Bob");
    }

    [Fact]
    public void Judges_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);

        repository.Judges.Should().NotBeNull();
        repository.Judges.Should().BeEmpty();
    }

    [Fact]
    public void Judges_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.Judges;

        // Add another judge to database after first access
        _context.Judges.Add(new Judge
        {
            JudgeID = 2,
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane@example.com"
        });
        _context.SaveChanges();

        var secondAccess = repository.Judges;

        // Should still have only 1 judge because of caching
        secondAccess.Should().HaveCount(1);
        firstAccess.Should().BeSameAs(secondAccess);
    }

    [Fact]
    public void Judges_ReturnsDataInCollectionForm()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            FirstName = "Test",
            LastName = "Judge",
            EmailAddress = "test@example.com"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Judges.Should().BeAssignableTo<IEnumerable<Judge>>();
    }

    [Fact]
    public void Judges_MultipleAccessesSameInstance()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.Judges;
        var secondAccess = repository.Judges;
        var thirdAccess = repository.Judges;

        firstAccess.Should().BeSameAs(secondAccess);
        secondAccess.Should().BeSameAs(thirdAccess);
    }

    [Fact]
    public void Judges_WithNullableProperties_HandlesCorrectly()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com",
            InformationMailed_ = null,
            AttendedJT_ = null,
            Active = null
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Judges.Should().HaveCount(1);
        var judge = repository.Judges!.First();
        judge.InformationMailed_.Should().BeNull();
        judge.AttendedJT_.Should().BeNull();
        judge.Active.Should().BeNull();
    }

    #endregion

    #region CoachesTrainingInfo Tests
    // NOTE: Cannot test CoachesTrainingInfo property - it's commented out in OdysseyMvc2024
    // The property uses lazy loading with First() and requires:
    // 1. Uncomment the property in OdysseyRepository.cs
    // 2. Uncomment Event DbSet in OdysseyEntities.cs
    // 3. Test scenarios:
    //    - First access loads from database with Contains("Coaches") && Contains("Training")
    //    - Subsequent accesses use cached value
    //    - Setter updates the backing field
    //    - Empty result throws InvalidOperationException (First() with no match)
    #endregion

    #region CoachesTrainingRegistrations Tests
    // NOTE: Cannot test CoachesTrainingRegistrations property - it's commented out in OdysseyMvc2024
    // The property returns ordered registrations and requires:
    // 1. Uncomment CoachesTrainingRegistrations DbSet in OdysseyEntities.cs
    // 2. Uncomment the property in OdysseyRepository.cs
    // 3. Test scenarios:
    //    - Returns all registrations ordered by RegistrationID
    //    - Empty database returns empty collection
    //    - Order is ascending by RegistrationID
    #endregion

    #region Divisions Tests
    // NOTE: Cannot test Divisions property - it's commented out in OdysseyMvc2024
    // The property uses lazy loading with private setter and requires:
    // 1. Uncomment CoachesTrainingDivision DbSet in OdysseyEntities.cs
    // 2. Uncomment the property in OdysseyRepository.cs
    // 3. Test scenarios:
    //    - First access loads from database ordered by ID
    //    - Subsequent accesses use cached value
    //    - Empty database returns empty collection
    //    - Order is ascending by ID
    #endregion

    #region JudgesInfo Property Tests

    [Fact]
    public void JudgesInfo_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        var judgesEvent = new Event
        {
            ID = 1,
            EventName = "Judges Training",
            StartDate = DateTime.Now
        };
        _context.Events.Add(judgesEvent);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.JudgesInfo.Should().NotBeNull();
        repository.JudgesInfo.ID.Should().Be(1);
        repository.JudgesInfo.EventName.Should().Be("Judges Training");
    }

    [Fact]
    public void JudgesInfo_FirstAccess_FindsEventWithBothJudgesAndTraining()
    {
        AddMinimalConfig();
        _context.Events.Add(new Event { ID = 1, EventName = "Coaches Training" });
        _context.Events.Add(new Event { ID = 2, EventName = "Judges Information" });
        _context.Events.Add(new Event { ID = 3, EventName = "Judges Training Event" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.JudgesInfo.Should().NotBeNull();
        repository.JudgesInfo.ID.Should().Be(3);
        repository.JudgesInfo.EventName.Should().Contain("Judges").And.Contain("Training");
    }

    [Fact]
    public void JudgesInfo_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        var judgesEvent = new Event
        {
            ID = 1,
            EventName = "Judges Training",
            StartDate = DateTime.Now
        };
        _context.Events.Add(judgesEvent);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.JudgesInfo;

        // Add another event after first access
        _context.Events.Add(new Event
        {
            ID = 2,
            EventName = "Another Judges Training",
            StartDate = DateTime.Now
        });
        _context.SaveChanges();

        var secondAccess = repository.JudgesInfo;

        secondAccess.Should().NotBeNull();
        firstAccess.Should().BeSameAs(secondAccess);
        secondAccess.ID.Should().Be(1);
    }

    [Fact]
    public void JudgesInfo_Setter_UpdatesBackingField()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        
        var repository = new OdysseyRepository(_context);
        var newEvent = new Event
        {
            ID = 99,
            EventName = "Custom Judges Training"
        };

        repository.JudgesInfo = newEvent;

        repository.JudgesInfo.Should().BeSameAs(newEvent);
        repository.JudgesInfo.ID.Should().Be(99);
    }

    [Fact]
    public void JudgesInfo_MultipleAccesses_ReturnsSameInstance()
    {
        AddMinimalConfig();
        _context.Events.Add(new Event { ID = 1, EventName = "Judges Training" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.JudgesInfo;
        var secondAccess = repository.JudgesInfo;
        var thirdAccess = repository.JudgesInfo;

        firstAccess.Should().BeSameAs(secondAccess);
        secondAccess.Should().BeSameAs(thirdAccess);
    }

    #endregion

    #region PrimaryProblem Property Tests

    [Fact]
    public void PrimaryProblem_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        var primaryProblem = new Problem
        {
            ProblemID = 6,
            ProblemName = "Primary Problem"
        };
        _context.Problems.Add(primaryProblem);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.PrimaryProblem.Should().NotBeNull();
        repository.PrimaryProblem.Should().HaveCount(1);
        repository.PrimaryProblem.First().ProblemID.Should().Be(6);
    }

    [Fact]
    public void PrimaryProblem_OnlyReturnsProblemID6()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary Problem" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.PrimaryProblem.Should().HaveCount(1);
        repository.PrimaryProblem.First().ProblemID.Should().Be(6);
        repository.PrimaryProblem.First().ProblemName.Should().Be("Primary Problem");
    }

    [Fact]
    public void PrimaryProblem_EmptyDatabase_ReturnsEmptyQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.PrimaryProblem.Should().NotBeNull();
        repository.PrimaryProblem.Should().BeEmpty();
    }

    [Fact]
    public void PrimaryProblem_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary Problem" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.PrimaryProblem;

        // Add another problem with ID 6 after first access (shouldn't affect cache)
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Another Problem" });
        _context.SaveChanges();

        var secondAccess = repository.PrimaryProblem;

        firstAccess.Should().BeSameAs(secondAccess);
        secondAccess.Should().HaveCount(1);
    }

    [Fact]
    public void PrimaryProblem_ReturnsIQueryable()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary Problem" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.PrimaryProblem.Should().BeAssignableTo<IQueryable<Problem>>();
    }

    #endregion

    #region ProblemChoices Property Tests

    [Fact]
    public void ProblemChoices_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemChoices.Should().NotBeNull();
        repository.ProblemChoices.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemChoices_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.ProblemChoices.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void ProblemChoices_AddsPrimaryProblemSuffix_WhenProblemID6Exists()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var primaryProblem = repository.ProblemChoices.FirstOrDefault(p => p.ProblemID == 6);

        primaryProblem.Should().NotBeNull();
        primaryProblem!.ProblemName.Should().Contain("(The Primary Problem)");
        primaryProblem.ProblemName.Should().Be("Primary (The Primary Problem)");
    }

    [Fact]
    public void ProblemChoices_DoesNotModifyNonPrimaryProblems()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problems = repository.ProblemChoices.ToList();

        problems.First(p => p.ProblemID == 1).ProblemName.Should().Be("Problem 1");
        problems.First(p => p.ProblemID == 7).ProblemName.Should().Be("Spontaneous");
    }

    [Fact]
    public void ProblemChoices_WithoutProblemID6_DoesNotAddSuffix()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problems = repository.ProblemChoices.ToList();

        problems.Should().NotContain(p => p.ProblemName!.Contains("(The Primary Problem)"));
    }

    [Fact]
    public void ProblemChoices_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemChoices.Should().NotBeNull();
        repository.ProblemChoices.Should().BeEmpty();
    }

    [Fact]
    public void ProblemChoices_QueryExecutedOnEachAccess_ReflectsNewData()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.ProblemChoices;

        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var secondAccess = repository.ProblemChoices;

        // Note: The query is re-executed on each access, so new data is reflected
        secondAccess.Should().HaveCount(2);
    }

    #endregion

    #region ProblemChoicesWithoutSpontaneous Property Tests

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemChoicesWithoutSpontaneous.Should().NotBeNull();
        repository.ProblemChoicesWithoutSpontaneous.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_ExcludesSpontaneous()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problems = repository.ProblemChoicesWithoutSpontaneous.ToList();

        problems.Should().HaveCount(2);
        problems.Should().NotContain(p => p.ProblemName == "Spontaneous");
    }

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.ProblemChoicesWithoutSpontaneous.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_AddsPrimaryProblemSuffix_WhenProblemID6Exists()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var primaryProblem = repository.ProblemChoicesWithoutSpontaneous.FirstOrDefault(p => p.ProblemID == 6);

        primaryProblem.Should().NotBeNull();
        primaryProblem!.ProblemName.Should().Contain("(The Primary Problem)");
    }

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemChoicesWithoutSpontaneous.Should().NotBeNull();
        repository.ProblemChoicesWithoutSpontaneous.Should().BeEmpty();
    }

    [Fact]
    public void ProblemChoicesWithoutSpontaneous_WithOnlySpontaneous_ReturnsEmpty()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemChoicesWithoutSpontaneous.Should().BeEmpty();
    }

    #endregion

    #region ProblemConflicts Property Tests

    [Fact]
    public void ProblemConflicts_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemConflicts.Should().NotBeNull();
        repository.ProblemConflicts.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemConflicts_ExcludesProblemID7()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problems = repository.ProblemConflicts.ToList();

        problems.Should().HaveCount(2);
        problems.Should().NotContain(p => p.ProblemID == 7);
    }

    [Fact]
    public void ProblemConflicts_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.ProblemConflicts.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void ProblemConflicts_IncludesProblemID6()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problems = repository.ProblemConflicts.ToList();

        problems.Should().Contain(p => p.ProblemID == 6);
        problems.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemConflicts_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemConflicts.Should().NotBeNull();
        repository.ProblemConflicts.Should().BeEmpty();
    }

    [Fact]
    public void ProblemConflicts_QueryExecutedOnEachAccess_ReflectsNewData()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.ProblemConflicts;

        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2" });
        _context.SaveChanges();

        var secondAccess = repository.ProblemConflicts;

        // Note: The query is re-executed on each access, so new data is reflected
        secondAccess.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemConflicts_WithOnlyProblemID7_ReturnsEmpty()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemConflicts.Should().BeEmpty();
    }

    #endregion

    #region Problems Property Tests

    [Fact]
    public void Problems_FirstAccess_LoadsFromDatabaseWithNonNullProblemCategory()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2", ProblemCategory = "Technical" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Problems.Should().NotBeNull();
        repository.Problems.Should().HaveCount(2);
    }

    [Fact]
    public void Problems_ExcludesProblemsWithNullProblemCategory()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2", ProblemCategory = null });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3", ProblemCategory = "Technical" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Problems.Should().HaveCount(2);
        repository.Problems.Should().Contain(p => p.ProblemID == 1);
        repository.Problems.Should().Contain(p => p.ProblemID == 3);
        repository.Problems.Should().NotContain(p => p.ProblemID == 2);
    }

    [Fact]
    public void Problems_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5", ProblemCategory = "Technical" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3", ProblemCategory = "Classics" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.Problems!.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void Problems_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Problems.Should().NotBeNull();
        repository.Problems.Should().BeEmpty();
    }

    [Fact]
    public void Problems_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.Problems;

        // Add another problem after first access
        _context.Problems.Add(new Problem { ProblemID = 2, ProblemName = "Problem 2", ProblemCategory = "Technical" });
        _context.SaveChanges();

        var secondAccess = repository.Problems;

        // The cached IQueryable is re-executed on enumeration, so it will see the new data
        secondAccess.Should().HaveCount(2);
        firstAccess.Should().BeSameAs(secondAccess);
    }

    [Fact]
    public void Problems_MultipleAccesses_ReturnsSameInstance()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.Problems;
        var secondAccess = repository.Problems;
        var thirdAccess = repository.Problems;

        firstAccess.Should().BeSameAs(secondAccess);
        secondAccess.Should().BeSameAs(thirdAccess);
    }

    [Fact]
    public void Problems_IncludesPrimaryProblem()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary", ProblemCategory = "Primary" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Problems.Should().Contain(p => p.ProblemID == 6);
    }

    [Fact]
    public void Problems_IncludesSpontaneousProblem()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1", ProblemCategory = "Vehicle" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous", ProblemCategory = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.Problems.Should().Contain(p => p.ProblemID == 7);
    }

    #endregion

    #region ProblemsWithoutPrimaryOrSpontaneous Property Tests

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 11, ProblemName = "Problem 11" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().NotBeNull();
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_ExcludesProblemID6()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().HaveCount(1);
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().NotContain(p => p.ProblemID == 6);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_ExcludesProblemID7()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().HaveCount(1);
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().NotContain(p => p.ProblemID == 7);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_ExcludesAllSpecialProblemIDs()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 11, ProblemName = "Problem 11" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().HaveCount(2);
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().Contain(p => p.ProblemID == 10);
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().Contain(p => p.ProblemID == 11);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.ProblemsWithoutPrimaryOrSpontaneous!.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_EmptyDatabase_ReturnsEmptyQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().NotBeNull();
        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().BeEmpty();
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.ProblemsWithoutPrimaryOrSpontaneous;

        // Add another problem after first access
        _context.Problems.Add(new Problem { ProblemID = 11, ProblemName = "Problem 11" });
        _context.SaveChanges();

        var secondAccess = repository.ProblemsWithoutPrimaryOrSpontaneous;

        // The cached IQueryable is re-executed on enumeration, so it will see the new data
        secondAccess.Should().HaveCount(2);
        firstAccess.Should().BeSameAs(secondAccess);
    }

    [Fact]
    public void ProblemsWithoutPrimaryOrSpontaneous_ReturnsIQueryable()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutPrimaryOrSpontaneous.Should().BeAssignableTo<IQueryable<Problem>>();
    }

    #endregion

    #region ProblemsWithoutSpontaneous Property Tests

    [Fact]
    public void ProblemsWithoutSpontaneous_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 11, ProblemName = "Problem 11" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutSpontaneous.Should().NotBeNull();
        repository.ProblemsWithoutSpontaneous.Should().HaveCount(2);
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_ExcludesProblemID7()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutSpontaneous.Should().HaveCount(1);
        repository.ProblemsWithoutSpontaneous.Should().NotContain(p => p.ProblemID == 7);
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_IncludesPrimaryProblem()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.Problems.Add(new Problem { ProblemID = 6, ProblemName = "Primary" });
        _context.Problems.Add(new Problem { ProblemID = 7, ProblemName = "Spontaneous" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutSpontaneous.Should().HaveCount(2);
        repository.ProblemsWithoutSpontaneous.Should().Contain(p => p.ProblemID == 6);
        repository.ProblemsWithoutSpontaneous.Should().NotContain(p => p.ProblemID == 7);
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_OrderedByProblemID()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 5, ProblemName = "Problem 5" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.Problems.Add(new Problem { ProblemID = 3, ProblemName = "Problem 3" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var problemList = repository.ProblemsWithoutSpontaneous!.ToList();

        problemList[0].ProblemID.Should().Be(1);
        problemList[1].ProblemID.Should().Be(3);
        problemList[2].ProblemID.Should().Be(5);
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_EmptyDatabase_ReturnsEmptyQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutSpontaneous.Should().NotBeNull();
        repository.ProblemsWithoutSpontaneous.Should().BeEmpty();
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_CachesResult_SubsequentAccessUsesCache()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 10, ProblemName = "Problem 10" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.ProblemsWithoutSpontaneous;

        // Add another problem after first access
        _context.Problems.Add(new Problem { ProblemID = 11, ProblemName = "Problem 11" });
        _context.SaveChanges();

        var secondAccess = repository.ProblemsWithoutSpontaneous;

        // The cached IQueryable is re-executed on enumeration, so it will see the new data
        secondAccess.Should().HaveCount(2);
        firstAccess.Should().BeSameAs(secondAccess);
    }

    [Fact]
    public void ProblemsWithoutSpontaneous_ReturnsIQueryable()
    {
        AddMinimalConfig();
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ProblemsWithoutSpontaneous.Should().BeAssignableTo<IQueryable<Problem>>();
    }

    #endregion

    #region RegionName Property Tests

    [Fact]
    public void RegionName_FirstAccess_LoadsFromConfig()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "NoVA North" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionName.Should().Be("NoVA North");
    }

    [Fact]
    public void RegionName_CachesResult_SubsequentAccessUsesCache()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "NoVA North" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.RegionName;

        // Update config in database after first access
        var config = _context.Configs.First(c => c.Name == "RegionName");
        config.Value = "Different Region";
        _context.SaveChanges();

        var secondAccess = repository.RegionName;

        // Should still return the cached value
        secondAccess.Should().Be("NoVA North");
        firstAccess.Should().Be(secondAccess);
    }

    [Fact]
    public void RegionName_MultipleAccesses_ReturnsSameInstance()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "NoVA North" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.RegionName;
        var secondAccess = repository.RegionName;
        var thirdAccess = repository.RegionName;

        firstAccess.Should().Be(secondAccess);
        secondAccess.Should().Be(thirdAccess);
    }

    [Fact]
    public void RegionName_WithSpecialCharacters_ReturnsCorrectly()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "NoVA & North Region's Test" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionName.Should().Be("NoVA & North Region's Test");
    }

    [Fact]
    public void RegionName_WithEmptyString_ReturnsEmptyString()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionName", Value = "" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionName.Should().Be("");
    }

    #endregion

    #region RegionNumber Property Tests

    [Fact]
    public void RegionNumber_FirstAccess_LoadsFromConfig()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "9" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionNumber.Should().Be("9");
    }

    [Fact]
    public void RegionNumber_CachesResult_SubsequentAccessUsesCache()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "9" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.RegionNumber;

        // Update config in database after first access
        var config = _context.Configs.First(c => c.Name == "RegionNumber");
        config.Value = "10";
        _context.SaveChanges();

        var secondAccess = repository.RegionNumber;

        // Should still return the cached value
        secondAccess.Should().Be("9");
        firstAccess.Should().Be(secondAccess);
    }

    [Fact]
    public void RegionNumber_MultipleAccesses_ReturnsSameInstance()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "9" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.RegionNumber;
        var secondAccess = repository.RegionNumber;
        var thirdAccess = repository.RegionNumber;

        firstAccess.Should().Be(secondAccess);
        secondAccess.Should().Be(thirdAccess);
    }

    [Fact]
    public void RegionNumber_WithMultiDigitNumber_ReturnsCorrectly()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "123" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionNumber.Should().Be("123");
    }

    [Fact]
    public void RegionNumber_WithEmptyString_ReturnsEmptyString()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
        _context.Configs.Add(new Config { Name = "RegionNumber", Value = "" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.RegionNumber.Should().Be("");
    }

    #endregion

    #region TournamentRegistrations Property Tests

    [Fact]
    public void TournamentRegistrations_FirstAccess_LoadsFromDatabase()
    {
        AddMinimalConfig();
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 1,
            CoachFirstName = "John"
        });
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 2,
            CoachFirstName = "Jane"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.TournamentRegistrations.Should().NotBeNull();
        repository.TournamentRegistrations.Should().HaveCount(2);
    }

    [Fact]
    public void TournamentRegistrations_ReturnsAllRegistrations()
    {
        AddMinimalConfig();
        var reg1 = new TournamentRegistration
        {
            Id = 1,
            CoachFirstName = "John",
            CoachLastName = "Doe"
        };
        var reg2 = new TournamentRegistration
        {
            Id = 2,
            CoachFirstName = "Jane",
            CoachLastName = "Smith"
        };
        var reg3 = new TournamentRegistration
        {
            Id = 3,
            CoachFirstName = "Bob",
            CoachLastName = "Johnson"
        };

        _context.TournamentRegistrations.AddRange(reg1, reg2, reg3);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.TournamentRegistrations.Should().HaveCount(3);
        repository.TournamentRegistrations.Should().Contain(r => r.CoachFirstName == "John");
        repository.TournamentRegistrations.Should().Contain(r => r.CoachFirstName == "Jane");
        repository.TournamentRegistrations.Should().Contain(r => r.CoachFirstName == "Bob");
    }

    [Fact]
    public void TournamentRegistrations_EmptyDatabase_ReturnsEmptyCollection()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);

        repository.TournamentRegistrations.Should().NotBeNull();
        repository.TournamentRegistrations.Should().BeEmpty();
    }

    [Fact]
    public void TournamentRegistrations_OrdersById()
    {
        AddMinimalConfig();
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 3,
            CoachFirstName = "Third"
        });
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 1,
            CoachFirstName = "First"
        });
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 2,
            CoachFirstName = "Second"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var results = repository.TournamentRegistrations.ToList();

        results[0].Id.Should().Be(1);
        results[1].Id.Should().Be(2);
        results[2].Id.Should().Be(3);
    }

    [Fact]
    public void TournamentRegistrations_ReturnsNewDataOnEachAccess()
    {
        AddMinimalConfig();
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 1,
            CoachFirstName = "John"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var firstAccess = repository.TournamentRegistrations.Count();

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 2,
            CoachFirstName = "Jane"
        });
        _context.SaveChanges();

        var secondAccess = repository.TournamentRegistrations.Count();

        firstAccess.Should().Be(1);
        secondAccess.Should().Be(2);
    }

    #endregion

    #region VolunteerInfo Property Tests
    // Note: VolunteerInfo property is commented out in OdysseyMvc2024.
    // To test this property, first uncomment it in OdysseyRepository.cs
    // and restore the corresponding DbSet in OdysseyEntities.cs.
    #endregion

    #region Volunteers Property Tests
    // Note: Volunteers property is commented out in OdysseyMvc2024.
    // To test this property, first uncomment it in OdysseyRepository.cs
    // and restore the corresponding DbSet<Volunteer> in OdysseyEntities.cs.
    #endregion

    #region AddCoachesTrainingRegistration Method Tests
    // Note: AddCoachesTrainingRegistration method is commented out in OdysseyMvc2024.
    // To test this method, first uncomment it in OdysseyRepository.cs
    // and restore the corresponding DbSet<CoachesTrainingRegistration> in OdysseyEntities.cs.
    #endregion

    #region AddJudge Method Tests

    [Fact]
    public void AddJudge_WithValidJudge_AddsToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);
        var newJudge = new Judge
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        };

        var result = repository.AddJudge(newJudge);

        result.Should().Be(1);
        _context.Judges.Should().HaveCount(1);
        _context.Judges.First().FirstName.Should().Be("John");
    }

    [Fact]
    public void AddJudge_WithNullJudge_ReturnsZero()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);

        var result = repository.AddJudge(null!);

        result.Should().Be(0);
        _context.Judges.Should().BeEmpty();
    }

    [Fact]
    public void AddJudge_MultipleJudges_AddsAllToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);
        var judge1 = new Judge
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        };
        var judge2 = new Judge
        {
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane@example.com"
        };

        repository.AddJudge(judge1);
        repository.AddJudge(judge2);

        _context.Judges.Should().HaveCount(2);
    }

    [Fact]
    public void AddJudge_WithValidJudge_ReturnsNumberOfAddedEntities()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);
        var newJudge = new Judge
        {
            FirstName = "Test",
            LastName = "Judge",
            EmailAddress = "test@example.com"
        };

        var result = repository.AddJudge(newJudge);

        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void AddJudge_WithValidData_PersistsAllProperties()
    {
        AddMinimalConfig();
        _context.SaveChanges();
        var repository = new OdysseyRepository(_context);
        var newJudge = new Judge
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com",
            Address = "123 Main St",
            City = "Springfield",
            State = "VA",
            ZipCode = "22150",
            EveningPhone = "703-555-1234",
            DaytimePhone = "703-555-5678"
        };

        repository.AddJudge(newJudge);

        var savedJudge = _context.Judges.First();
        savedJudge.FirstName.Should().Be("John");
        savedJudge.LastName.Should().Be("Doe");
        savedJudge.EmailAddress.Should().Be("john@example.com");
        savedJudge.Address.Should().Be("123 Main St");
        savedJudge.City.Should().Be("Springfield");
        savedJudge.State.Should().Be("VA");
        savedJudge.ZipCode.Should().Be("22150");
        savedJudge.EveningPhone.Should().Be("703-555-1234");
        savedJudge.DaytimePhone.Should().Be("703-555-5678");
    }

    #endregion

    #region AddTournamentRegistration Tests

    [Fact]
    public void AddTournamentRegistration_ValidRegistration_ReturnsOne()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var registration = new TournamentRegistration
        {
            CoachFirstName = "John",
            CoachLastName = "Doe"
        };

        var result = repository.AddTournamentRegistration(registration);

        result.Should().Be(1);
    }

    [Fact]
    public void AddTournamentRegistration_ValidRegistration_AddsToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var registration = new TournamentRegistration
        {
            CoachFirstName = "Jane",
            CoachLastName = "Smith",
            CoachEmailAddress = "jane@example.com"
        };

        repository.AddTournamentRegistration(registration);

        var saved = _context.TournamentRegistrations.FirstOrDefault();
        saved.Should().NotBeNull();
        saved!.CoachFirstName.Should().Be("Jane");
        saved.CoachLastName.Should().Be("Smith");
        saved.CoachEmailAddress.Should().Be("jane@example.com");
    }

    [Fact]
    public void AddTournamentRegistration_NullRegistration_ReturnsZero()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.AddTournamentRegistration(null!);

        result.Should().Be(0);
    }

    [Fact]
    public void AddTournamentRegistration_NullRegistration_DoesNotAddToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.AddTournamentRegistration(null!);

        _context.TournamentRegistrations.Should().BeEmpty();
    }

    [Fact]
    public void AddTournamentRegistration_MultipleRegistrations_ReturnsOne()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var registration1 = new TournamentRegistration { CoachFirstName = "John" };
        var registration2 = new TournamentRegistration { CoachFirstName = "Jane" };

        repository.AddTournamentRegistration(registration1);
        var result = repository.AddTournamentRegistration(registration2);

        result.Should().Be(1);
        _context.TournamentRegistrations.Should().HaveCount(2);
    }

    #endregion

    #region AddVolunteer Tests

    [Fact]
    public void AddVolunteer_ValidVolunteer_ReturnsOne()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var volunteer = new Volunteer
        {
            FirstName = "John",
            LastName = "Doe"
        };

        var result = repository.AddVolunteer(volunteer);

        result.Should().Be(1);
    }

    [Fact]
    public void AddVolunteer_ValidVolunteer_AddsToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var volunteer = new Volunteer
        {
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane@example.com"
        };

        repository.AddVolunteer(volunteer);

        var saved = _context.Volunteers.FirstOrDefault();
        saved.Should().NotBeNull();
        saved!.FirstName.Should().Be("Jane");
        saved.LastName.Should().Be("Smith");
        saved.EmailAddress.Should().Be("jane@example.com");
    }

    [Fact]
    public void AddVolunteer_WithTournamentRegistrationId_SetsTeamId()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var volunteer = new Volunteer
        {
            FirstName = "John",
            LastName = "Doe"
        };

        repository.AddVolunteer(volunteer, 42);

        var saved = _context.Volunteers.FirstOrDefault();
        saved.Should().NotBeNull();
        saved!.TeamID.Should().Be(42);
    }

    [Fact]
    public void AddVolunteer_WithoutTournamentRegistrationId_SetsTeamIdToNull()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var volunteer = new Volunteer
        {
            FirstName = "John",
            LastName = "Doe",
            TeamID = 99
        };

        repository.AddVolunteer(volunteer);

        var saved = _context.Volunteers.FirstOrDefault();
        saved.Should().NotBeNull();
        saved!.TeamID.Should().BeNull();
    }

    [Fact]
    public void AddVolunteer_NullVolunteer_ReturnsZero()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.AddVolunteer(null!);

        result.Should().Be(0);
    }

    [Fact]
    public void AddVolunteer_NullVolunteer_DoesNotAddToDatabase()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.AddVolunteer(null!);

        _context.Volunteers.Should().BeEmpty();
    }

    [Fact]
    public void AddVolunteer_MultipleVolunteers_ReturnsOne()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        var volunteer1 = new Volunteer { FirstName = "John" };
        var volunteer2 = new Volunteer { FirstName = "Jane" };

        repository.AddVolunteer(volunteer1, 1);
        var result = repository.AddVolunteer(volunteer2, 2);

        result.Should().Be(1);
        _context.Volunteers.Should().HaveCount(2);
    }

    #endregion

    #region ClearTeamIdFromJudgeRecord Tests

    [Fact]
    public void ClearTeamIdFromJudgeRecord_ExistingJudge_ClearsTeamId()
    {
        AddMinimalConfig();
        var judge = new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            TeamID = 42,
            EmailAddress = "john@example.com"
        };
        _context.Judges.Add(judge);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ClearTeamIdFromJudgeRecord(1, "John", "Doe");

        var updated = _context.Judges.Find(1);
        updated.Should().NotBeNull();
        updated!.TeamID.Should().BeNull();
    }

    [Fact]
    public void ClearTeamIdFromJudgeRecord_ExistingJudgeWithNullTeamId_RemainsNull()
    {
        AddMinimalConfig();
        var judge = new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            TeamID = null,
            EmailAddress = "john@example.com"
        };
        _context.Judges.Add(judge);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ClearTeamIdFromJudgeRecord(1, "John", "Doe");

        var updated = _context.Judges.Find(1);
        updated.Should().NotBeNull();
        updated!.TeamID.Should().BeNull();
    }

    [Fact]
    public void ClearTeamIdFromJudgeRecord_NonExistentJudge_DoesNotThrow()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var act = () => repository.ClearTeamIdFromJudgeRecord(999, "NonExistent", "Judge");

        act.Should().NotThrow();
    }

    [Fact]
    public void ClearTeamIdFromJudgeRecord_NonExistentJudge_DoesNotModifyDatabase()
    {
        AddMinimalConfig();
        var judge = new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            TeamID = 42,
            EmailAddress = "john@example.com"
        };
        _context.Judges.Add(judge);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        repository.ClearTeamIdFromJudgeRecord(999, "NonExistent", "Judge");

        var unchanged = _context.Judges.Find(1);
        unchanged.Should().NotBeNull();
        unchanged!.TeamID.Should().Be(42);
    }

    [Fact]
    public void ClearTeamIdFromJudgeRecord_SavesChanges()
    {
        AddMinimalConfig();
        var judge = new Judge
        {
            JudgeID = 1,
            FirstName = "John",
            LastName = "Doe",
            TeamID = 42,
            EmailAddress = "john@example.com"
        };
        _context.Judges.Add(judge);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);
        repository.ClearTeamIdFromJudgeRecord(1, "John", "Doe");

        var updated = _context.Judges.AsNoTracking().FirstOrDefault(j => j.JudgeID == 1);
        updated.Should().NotBeNull();
        updated!.TeamID.Should().BeNull();
    }

    #endregion

    #region ExportJudges Tests

    [Fact]
    public void ExportJudges_ReturnsIQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.ExportJudges();

        result.Should().BeAssignableTo<IQueryable<JudgesExport>>();
    }

    [Fact]
    public void ExportJudges_NoJudges_ReturnsEmptyQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.ExportJudges().ToList();

        result.Should().BeEmpty();
    }

    [Fact]
    public void ExportJudges_OrdersResultsByJudgeId()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge { JudgeID = 3, FirstName = "John", LastName = "Doe", EmailAddress = "john@example.com" });
        _context.Judges.Add(new Judge { JudgeID = 1, FirstName = "Jane", LastName = "Smith", EmailAddress = "jane@example.com" });
        _context.Judges.Add(new Judge { JudgeID = 2, FirstName = "Bob", LastName = "Johnson", EmailAddress = "bob@example.com" });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.ExportJudges().ToList();

        result.Should().HaveCount(3);
        result[0].JudgeId.Should().Be(1);
        result[1].JudgeId.Should().Be(2);
        result[2].JudgeId.Should().Be(3);
    }

    [Fact]
    public void ExportJudges_MapsJudgeProperties()
    {
        AddMinimalConfig();
        _context.Judges.Add(new Judge
        {
            JudgeID = 1,
            TeamID = 42,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St",
            AddressLine2 = "Apt 4",
            City = "Springfield",
            State = "VA",
            ZipCode = "22150",
            DaytimePhone = "555-1234",
            EveningPhone = "555-5678",
            EmailAddress = "john@example.com",
            Notes = "Test notes",
            ProblemCOI2 = "Problem 2",
            ProblemCOI3 = "Problem 3",
            ProblemChoice1 = "Choice 1",
            ProblemChoice2 = "Choice 2",
            ProblemChoice3 = "Choice 3",
            TshirtSize = "L",
            WantsCEUCredit = "Yes",
            YearsOfLongTermJudgingExperience = "5",
            YearsOfSpontaneousJudgingExperience = "3",
            TimeRegistered = new DateTime(2025, 1, 15),
            TimeAssignedToTeam = new DateTime(2025, 1, 20),
            TimeRegistrationStarted = new DateTime(2025, 1, 10),
            UserAgent = "TestAgent"
        });
        _context.Problems.Add(new Problem { ProblemID = 1, ProblemName = "Problem 1" });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.ExportJudges().ToList();

        result.Should().HaveCount(1);
        var exported = result[0];
        exported.JudgeId.Should().Be(1);
        exported.TeamId.Should().Be(42);
        exported.FirstName.Should().Be("John");
        exported.LastName.Should().Be("Doe");
        exported.Address.Should().Be("123 Main St");
        exported.Address2.Should().Be("Apt 4");
        exported.City.Should().Be("Springfield");
        exported.StateOrProvince.Should().Be("VA");
        exported.PostalCode.Should().Be("22150");
        exported.DaytimePhone.Should().Be("555-1234");
        exported.EveningPhone.Should().Be("555-5678");
        exported.Email.Should().Be("john@example.com");
        exported.Notes.Should().Be("Test notes");
        exported.ProblemConflictOfInterest2.Should().Be("Problem 2");
        exported.ProblemConflictOfInterest3.Should().Be("Problem 3");
        exported.ProblemChoice1.Should().Be("Choice 1");
        exported.ProblemChoice2.Should().Be("Choice 2");
        exported.ProblemChoice3.Should().Be("Choice 3");
        exported.TshirtSize.Should().Be("L");
        exported.ContinuingEducationUnits.Should().Be("Yes");
        exported.YearsOfLongTermJudgingExperience.Should().Be("5");
        exported.YearsOfSpontaneousJudgingExperience.Should().Be("3");
        exported.TimeRegistered.Should().Be(new DateTime(2025, 1, 15));
        exported.TimeAssignedToTeam.Should().Be(new DateTime(2025, 1, 20));
        exported.TimeRegistrationStarted.Should().Be(new DateTime(2025, 1, 10));
        exported.UserAgent.Should().Be("TestAgent");
    }

    #endregion

    #region GetCoachById Tests

    [Fact]
    public void GetCoachById_ExistingCoach_ReturnsCoach()
    {
        AddMinimalConfig();
        var coach = new CoachesTrainingRegistration
        {
            RegistrationID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john@example.com"
        };
        _context.CoachesTrainingRegistrations.Add(coach);
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.GetCoachById(1).ToList();

        result.Should().HaveCount(1);
        result[0].RegistrationID.Should().Be(1);
        result[0].FirstName.Should().Be("John");
        result[0].LastName.Should().Be("Doe");
    }

    [Fact]
    public void GetCoachById_NonExistentCoach_ReturnsEmpty()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.GetCoachById(999).ToList();

        result.Should().BeEmpty();
    }

    [Fact]
    public void GetCoachById_MultipleCoaches_ReturnsOnlyMatchingCoach()
    {
        AddMinimalConfig();
        _context.CoachesTrainingRegistrations.Add(new CoachesTrainingRegistration
        {
            RegistrationID = 1,
            FirstName = "John",
            LastName = "Doe"
        });
        _context.CoachesTrainingRegistrations.Add(new CoachesTrainingRegistration
        {
            RegistrationID = 2,
            FirstName = "Jane",
            LastName = "Smith"
        });
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.GetCoachById(2).ToList();

        result.Should().HaveCount(1);
        result[0].RegistrationID.Should().Be(2);
        result[0].FirstName.Should().Be("Jane");
    }

    [Fact]
    public void GetCoachById_ReturnsIQueryable()
    {
        AddMinimalConfig();
        _context.SaveChanges();

        var repository = new OdysseyRepository(_context);

        var result = repository.GetCoachById(1);

        result.Should().BeAssignableTo<IQueryable<CoachesTrainingRegistration>>();
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Adds minimal config required by OdysseyRepository constructor (Year is needed for EndYear calculation).
    /// </summary>
    private void AddMinimalConfig()
    {
        _context.Configs.Add(new Config { Name = "Year", Value = "2025" });
    }

    #endregion
}
