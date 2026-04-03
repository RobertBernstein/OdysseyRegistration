using FluentAssertions;
using OdysseyMvc4.ViewData.JudgesRegistration;
using System.Web.Mvc;

namespace OdysseyMvc4.UnitTests.ViewData.JudgesRegistration;

/// <summary>
/// Tests for Page02ViewData properties.
/// </summary>
public class Page02ViewDataTests
{
    #region ProblemConflictList1 Tests

    [Fact]
    public void ProblemConflictList1_WithProblemChoices_ReturnsListWithNoPreferenceTransformedToIDontKnow()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "No Preference", Value = "0" },
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList1;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().Contain(item => item.Text == "I Don't Know");
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
        result.Should().NotContain(item => item.Text == "No Preference");
    }

    [Fact]
    public void ProblemConflictList1_WithoutNoPreference_ReturnsUnmodifiedList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList1;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
    }

    [Fact]
    public void ProblemConflictList1_WithEmptyProblemChoices_ReturnsEmptyList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>()
        };

        // Act
        var result = viewData.ProblemConflictList1;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void ProblemConflictList1_WithNullProblemChoices_ThrowsException()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = null
        };

        // Act
        var act = () => viewData.ProblemConflictList1.ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region ProblemConflictList2 Tests

    [Fact]
    public void ProblemConflictList2_WithProblemChoices_ReturnsListWithNoPreferenceTransformedToIDontKnow()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "No Preference", Value = "0" },
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList2;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().Contain(item => item.Text == "I Don't Know");
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
        result.Should().NotContain(item => item.Text == "No Preference");
    }

    [Fact]
    public void ProblemConflictList2_WithoutNoPreference_ReturnsUnmodifiedList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList2;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
    }

    [Fact]
    public void ProblemConflictList2_WithEmptyProblemChoices_ReturnsEmptyList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>()
        };

        // Act
        var result = viewData.ProblemConflictList2;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void ProblemConflictList2_WithNullProblemChoices_ThrowsException()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = null
        };

        // Act
        var act = () => viewData.ProblemConflictList2.ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region ProblemConflictList3 Tests

    [Fact]
    public void ProblemConflictList3_WithProblemChoices_ReturnsListWithNoPreferenceTransformedToIDontKnow()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "No Preference", Value = "0" },
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList3;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().Contain(item => item.Text == "I Don't Know");
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
        result.Should().NotContain(item => item.Text == "No Preference");
    }

    [Fact]
    public void ProblemConflictList3_WithoutNoPreference_ReturnsUnmodifiedList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>
            {
                new SelectListItem { Text = "Problem 1", Value = "1" },
                new SelectListItem { Text = "Problem 2", Value = "2" }
            }
        };

        // Act
        var result = viewData.ProblemConflictList3;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(item => item.Text == "Problem 1");
        result.Should().Contain(item => item.Text == "Problem 2");
    }

    [Fact]
    public void ProblemConflictList3_WithEmptyProblemChoices_ReturnsEmptyList()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = new List<SelectListItem>()
        };

        // Act
        var result = viewData.ProblemConflictList3;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void ProblemConflictList3_WithNullProblemChoices_ThrowsException()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            ProblemChoices = null
        };

        // Act
        var act = () => viewData.ProblemConflictList3.ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion
}
