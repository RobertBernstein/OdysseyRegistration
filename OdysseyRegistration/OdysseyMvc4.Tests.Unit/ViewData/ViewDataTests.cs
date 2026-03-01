using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;
using OdysseyMvc2024.ViewData.JudgesRegistration;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using OdysseyMvc4.Tests.Unit.Helpers;
using Page01ViewDataJudges = OdysseyMvc2024.ViewData.JudgesRegistration.Page01ViewData;
using Page01ViewDataTournament = OdysseyMvc2024.ViewData.TournamentRegistration.Page01ViewData;
using Page02ViewDataJudges = OdysseyMvc2024.ViewData.JudgesRegistration.Page02ViewData;

namespace OdysseyMvc4.Tests.Unit.ViewData;

/// <summary>
/// Tests for ViewData computed properties that must be preserved during migration.
/// </summary>
public class ViewDataTests
{
    #region BaseViewData.TournamentDate Tests

    [Fact]
    public void TournamentDate_WithValidDate_ReturnsLongDateString()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { StartDate = new DateTime(2025, 3, 15) }
        };

        viewData.TournamentDate.Should().Be(new DateTime(2025, 3, 15).ToLongDateString());
    }

    [Fact]
    public void TournamentDate_WithNullDate_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { StartDate = null }
        };

        viewData.TournamentDate.Should().Be("TBA");
    }

    [Fact]
    public void TournamentDate_WithNullTournamentInfo_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = null
        };

        viewData.TournamentDate.Should().Be("TBA");
    }

    #endregion

    #region BaseViewData.TournamentLocation Tests

    [Fact]
    public void TournamentLocation_WithValidLocation_ReturnsLocation()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = "Springfield High School" }
        };

        viewData.TournamentLocation.Should().Be("Springfield High School");
    }

    [Fact]
    public void TournamentLocation_WithNullLocation_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = null }
        };

        viewData.TournamentLocation.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_WithEmptyLocation_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = "" }
        };

        viewData.TournamentLocation.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_WithWhitespaceLocation_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = "   " }
        };

        viewData.TournamentLocation.Should().Be("TBA");
    }

    #endregion

    #region BaseViewData.TournamentTime Tests

    [Fact]
    public void TournamentTime_WithValidTime_ReturnsTime()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = "8:00 AM" }
        };

        viewData.TournamentTime.Should().Be("8:00 AM");
    }

    [Fact]
    public void TournamentTime_WithNullTime_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = null }
        };

        viewData.TournamentTime.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_WithEmptyTime_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = "" }
        };

        viewData.TournamentTime.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_WithWhitespaceTime_ReturnsTBA()
    {
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = "   " }
        };

        viewData.TournamentTime.Should().Be("TBA");
    }

    #endregion

    #region Judges Page01ViewData TBA Fallback Tests

    [Fact]
    public void JudgesTrainingDate_WithValidDate_ReturnsLongDateString()
    {
        var judgesInfo = TestHelper.CreateDefaultJudgesInfo();
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = judgesInfo,
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingDate.Should().Be(judgesInfo.StartDate!.Value.ToLongDateString());
    }

    [Fact]
    public void JudgesTrainingDate_WithNullDate_ReturnsTBA()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { StartDate = null },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingDate.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_WithValidLocation_ReturnsLocation()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { Location = "Community Center" },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingLocation.Should().Be("Community Center");
    }

    [Fact]
    public void JudgesTrainingLocation_WithNullLocation_ReturnsTBA()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { Location = null },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingLocation.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_WithEmptyLocation_ReturnsTBA()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { Location = "" },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingLocation.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_WithValidTime_ReturnsTime()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { Time = "9:00 AM" },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingTime.Should().Be("9:00 AM");
    }

    [Fact]
    public void JudgesTrainingTime_WithNullTime_ReturnsTBA()
    {
        var viewData = new Page01ViewDataJudges
        {
            JudgesInfo = new Event { Time = null },
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };

        viewData.JudgesTrainingTime.Should().Be("TBA");
    }

    #endregion

    #region Tournament Page01ViewData Tests

    [Fact]
    public void TeamRegistrationFee_BeforeLateFeeDate_ReturnsRegularFee()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.EventCost = "100";
        viewData.TournamentInfo.LateEventCost = "125";
        viewData.TournamentInfo.LateEventCostStartDate = DateTime.Now.AddDays(30); // late fee hasn't started yet

        viewData.TeamRegistrationFee.Should().Be("$100");
    }

    [Fact]
    public void TeamRegistrationFee_AfterLateFeeDate_ReturnsLateFee()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.EventCost = "100";
        viewData.TournamentInfo.LateEventCost = "125";
        viewData.TournamentInfo.LateEventCostStartDate = DateTime.Now.AddDays(-1); // late fee has started

        viewData.TeamRegistrationFee.Should().Be("$125");
    }

    [Fact]
    public void TeamRegistrationFee_NoLateEventCostStartDate_ReturnsRegularFee()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.EventCost = "100";
        viewData.TournamentInfo.LateEventCost = "125";
        viewData.TournamentInfo.LateEventCostStartDate = null;

        viewData.TeamRegistrationFee.Should().Be("$100");
    }

    [Fact]
    public void TeamRegistrationFee_NullEventCost_ReturnsTBA()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.EventCost = null;
        viewData.TournamentInfo.LateEventCostStartDate = null;

        viewData.TeamRegistrationFee.Should().Be("TBA");
    }

    [Fact]
    public void TeamRegistrationFee_EmptyEventCost_ReturnsTBA()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.EventCost = "";
        viewData.TournamentInfo.LateEventCostStartDate = null;

        viewData.TeamRegistrationFee.Should().Be("TBA");
    }

    [Fact]
    public void LateTeamRegistrationFee_WithValue_ReturnsDollarAmount()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.LateEventCost = "125";

        viewData.LateTeamRegistrationFee.Should().Be("$125");
    }

    [Fact]
    public void LateTeamRegistrationFee_WithoutValue_ReturnsEmpty()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.LateEventCost = null;

        viewData.LateTeamRegistrationFee.Should().BeEmpty();
    }

    [Fact]
    public void PaymentDueDate_WithValidDate_ReturnsLongDateString()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.PaymentDueDate = new DateTime(2025, 3, 10);

        viewData.PaymentDueDate.Should().Be(new DateTime(2025, 3, 10).ToLongDateString());
    }

    [Fact]
    public void PaymentDueDate_WithNullDate_ReturnsTBA()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.PaymentDueDate = null;

        viewData.PaymentDueDate.Should().Be("TBA");
    }

    [Fact]
    public void LateEventCostStartDate_WithValidDate_ReturnsDateMinusOneDay()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.LateEventCostStartDate = new DateTime(2025, 3, 1);

        // The property returns the day BEFORE the late cost start date
        viewData.LateEventCostStartDate.Should().Be(new DateTime(2025, 2, 28).ToLongDateString());
    }

    [Fact]
    public void LateEventCostStartDate_WithNullDate_ReturnsTBA()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.TournamentInfo!.LateEventCostStartDate = null;

        viewData.LateEventCostStartDate.Should().Be("TBA");
    }

    [Fact]
    public void AcceptingPayPal_WhenTrue_ReturnsTrue()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.Config!["AcceptingPayPal"] = "true";

        viewData.AcceptingPayPal.Should().BeTrue();
    }

    [Fact]
    public void AcceptingPayPal_WhenFalse_ReturnsFalse()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.Config!["AcceptingPayPal"] = "false";

        viewData.AcceptingPayPal.Should().BeFalse();
    }

    [Fact]
    public void AcceptingPayPal_WhenInvalidValue_ReturnsFalse()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.Config!["AcceptingPayPal"] = "not-a-boolean";

        viewData.AcceptingPayPal.Should().BeFalse();
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithValidDate_ReturnsLongDateString()
    {
        var viewData = CreateTournamentPage01ViewData();
        viewData.Config!["TournamentRegistrationCloseDateTime"] = "12/31/2025 23:59:59";

        viewData.TournamentRegistrationCloseDateTime.Should().Be(new DateTime(2025, 12, 31).ToLongDateString());
    }

    #endregion

    #region Judges Page02ViewData ProblemConflictList Tests

    [Fact]
    public void ProblemConflictList1_TransformsNoPreferenceToIDontKnow()
    {
        var problemChoices = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Problem A" },
            new() { Value = "2", Text = "Problem B" },
            new() { Value = "0", Text = "No Preference" }
        };

        var viewData = new Page02ViewDataJudges
        {
            TshirtSizes = [],
            ProblemChoices = new SelectList(problemChoices, "Value", "Text")
        };

        var conflicts = viewData.ProblemConflictList1.ToList();
        conflicts.Should().Contain(item => item.Text == "I Don't Know");
        conflicts.Should().NotContain(item => item.Text == "No Preference");
    }

    [Fact]
    public void ProblemConflictList2_TransformsNoPreferenceToIDontKnow()
    {
        var problemChoices = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Problem A" },
            new() { Value = "0", Text = "No Preference" }
        };

        var viewData = new Page02ViewDataJudges
        {
            TshirtSizes = [],
            ProblemChoices = new SelectList(problemChoices, "Value", "Text")
        };

        var conflicts = viewData.ProblemConflictList2.ToList();
        conflicts.Should().Contain(item => item.Text == "I Don't Know");
    }

    [Fact]
    public void ProblemConflictList3_TransformsNoPreferenceToIDontKnow()
    {
        var problemChoices = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Problem A" },
            new() { Value = "0", Text = "No Preference" }
        };

        var viewData = new Page02ViewDataJudges
        {
            TshirtSizes = [],
            ProblemChoices = new SelectList(problemChoices, "Value", "Text")
        };

        var conflicts = viewData.ProblemConflictList3.ToList();
        conflicts.Should().Contain(item => item.Text == "I Don't Know");
    }

    [Fact]
    public void ProblemConflictList_WithoutNoPreference_LeavesItemsUnchanged()
    {
        var problemChoices = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Problem A" },
            new() { Value = "2", Text = "Problem B" }
        };

        var viewData = new Page02ViewDataJudges
        {
            TshirtSizes = [],
            ProblemChoices = new SelectList(problemChoices, "Value", "Text")
        };

        var conflicts = viewData.ProblemConflictList1.ToList();
        conflicts.Should().HaveCount(2);
        conflicts.Should().Contain(item => item.Text == "Problem A");
        conflicts.Should().Contain(item => item.Text == "Problem B");
    }

    [Fact]
    public void ProblemConflictList_PreservesOtherItems()
    {
        var problemChoices = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Problem A" },
            new() { Value = "2", Text = "Problem B" },
            new() { Value = "0", Text = "No Preference" }
        };

        var viewData = new Page02ViewDataJudges
        {
            TshirtSizes = [],
            ProblemChoices = new SelectList(problemChoices, "Value", "Text")
        };

        var conflicts = viewData.ProblemConflictList1.ToList();
        conflicts.Should().HaveCount(3);
        conflicts.Should().Contain(item => item.Text == "Problem A");
        conflicts.Should().Contain(item => item.Text == "Problem B");
        conflicts.Should().Contain(item => item.Text == "I Don't Know");
    }

    #endregion

    #region Helper Methods

    private static Page01ViewDataTournament CreateTournamentPage01ViewData()
    {
        return new Page01ViewDataTournament
        {
            Config = TestHelper.CreateDefaultConfig(),
            TournamentInfo = TestHelper.CreateDefaultTournamentInfo()
        };
    }

    #endregion
}
