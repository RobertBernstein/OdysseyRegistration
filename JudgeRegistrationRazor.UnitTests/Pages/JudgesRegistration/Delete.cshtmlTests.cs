using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.JudgesRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Pages.JudgesRegistration;

public class DeleteModelTests
{
    private static OdysseyContext CreateInMemoryContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
        
        return new TestOdysseyContext(options);
    }

    [Fact]
    public void Constructor_WithValidContext_SetsContext()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_Constructor");

        // Act
        var model = new DeleteModel(context);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public async Task OnGetAsync_NullId_ReturnsNotFound()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnGetAsync_NullId");
        var model = new DeleteModel(context);

        // Act
        var result = await model.OnGetAsync(null);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OnGetAsync_JudgeNotFound_ReturnsNotFound()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnGetAsync_JudgeNotFound");
        var model = new DeleteModel(context);

        // Act
        var result = await model.OnGetAsync(999);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OnGetAsync_JudgeExists_SetsJudgeAndReturnsPage()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnGetAsync_JudgeExists");
        context.Judges.Add(new Judge { Id = 1, TeamId = "Team1" });
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var model = new DeleteModel(context);
        var result = await model.OnGetAsync(1);

        // Assert
        result.Should().BeOfType<PageResult>();
        model.Judge.Should().NotBeNull();
        model.Judge.Id.Should().Be(1);
        model.Judge.TeamId.Should().Be("Team1");
    }

    [Fact]
    public async Task OnPostAsync_NullId_ReturnsNotFound()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnPostAsync_NullId");
        var model = new DeleteModel(context);

        // Act
        var result = await model.OnPostAsync(null);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OnPostAsync_JudgeNotFound_ReturnsRedirectToPage()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnPostAsync_JudgeNotFound");
        var model = new DeleteModel(context);

        // Act
        var result = await model.OnPostAsync(999);

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("./Index");
    }

    [Fact]
    public async Task OnPostAsync_JudgeExists_RemovesJudgeAndReturnsRedirectToPage()
    {
        // Arrange
        var context = CreateInMemoryContext("TestDb_OnPostAsync_JudgeExists");
        context.Judges.Add(new Judge { Id = 1, TeamId = "Team1" });
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var model = new DeleteModel(context);
        var result = await model.OnPostAsync(1);

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("./Index");
        model.Judge.Should().NotBeNull();
        model.Judge.Id.Should().Be(1);

        // Verify judge was actually deleted
        var deletedJudge = await context.Judges.FindAsync(new object[] { 1 }, TestContext.Current.CancellationToken);
        deletedJudge.Should().BeNull();
    }

    // Inner test context that doesn't seed data
    private class TestOdysseyContext : OdysseyContext
    {
        public TestOdysseyContext(DbContextOptions<OdysseyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base but skip the seeding part by not calling HasData
            modelBuilder.Entity<CoachesTrainingDivision>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_coaches_training_divisions");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_config");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Value).HasMaxLength(800);
                // Skip: entity.HasData(SeedHelper.SeedData<Config>("Config.json"));
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_events");
                // Skip: entity.HasData(SeedHelper.SeedData<Event>("Events.json"));
            });

            modelBuilder.Entity<Judge>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_judges");
            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_problems");
                // Skip: entity.HasData(SeedHelper.SeedData<Problem>("Problem.json"));
            });

            modelBuilder.Entity<School>(entity =>
            {
                // Skip: entity.HasData(SeedHelper.SeedData<School>("Schools.json"));
            });

            modelBuilder.Entity<TournamentRegistration>(entity =>
            {
                entity.ToTable("TournamentRegistration");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_volunteers");
            });
        }
    }
}
