// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPaymentStatusReportTests.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Tests for tournament registration payment status reporting.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using System.Globalization;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPaymentStatusReportTests
{
    [Fact]
    public void PaymentStatusReport_WhenFilterIsPaid_ReturnsOnlyPaidTeams()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.PaymentStatusReport(PaymentStatusReportViewData.PaidFilterValue);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<PaymentStatusReportViewData>().Subject;

        model.PaidFilter.Should().Be(PaymentStatusReportViewData.PaidFilterValue);
        model.Rows.Should().HaveCount(1);
        model.Rows[0].TeamId.Should().Be(2);
        model.Rows[0].Paid.Should().BeTrue();
    }

    [Fact]
    public void PaymentStatusReport_WhenFilterIsUnpaid_ReturnsOnlyUnpaidTeams()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.PaymentStatusReport(PaymentStatusReportViewData.UnpaidFilterValue);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<PaymentStatusReportViewData>().Subject;

        model.PaidFilter.Should().Be(PaymentStatusReportViewData.UnpaidFilterValue);
        model.Rows.Should().HaveCount(2);
        model.Rows.Should().OnlyContain(row => !row.Paid);
    }

    [Fact]
    public void PaymentStatusReport_WhenFilterIsInvalid_DefaultsToAllTeams()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.PaymentStatusReport("not-a-filter");

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<PaymentStatusReportViewData>().Subject;

        model.PaidFilter.Should().Be(PaymentStatusReportViewData.AllFilterValue);
        model.Rows.Should().HaveCount(3);
        model.Rows.Select(row => row.TeamId).Should().ContainInOrder(1, 2, 3);
    }

    private static TournamentRegistrationController CreateController(IOdysseyRepository repository)
    {
        var controller = new TournamentRegistrationController(repository);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString("www.novanorth.org");

        var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
        controller.ControllerContext = new ControllerContext(actionContext);

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.SetupGet(x => x.ActionContext).Returns(actionContext);
        urlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns((string value) => value);
        urlHelper.Setup(x => x.RouteUrl(It.IsAny<UrlRouteContext>())).Returns(string.Empty);
        controller.Url = urlHelper.Object;

        controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

        return controller;
    }

    private static Mock<IOdysseyRepository> CreateRepository()
    {
        var now = DateTime.Now;

        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(new Dictionary<string, string>
        {
            ["TournamentRegistrationOpenDateTime"] = now.AddDays(-1).ToString("O", CultureInfo.InvariantCulture),
            ["TournamentRegistrationCloseDateTime"] = now.AddDays(1).ToString("O", CultureInfo.InvariantCulture),
            ["IsTournamentRegistrationDown"] = "false",
            ["HomePage"] = "https://www.novanorth.org"
        });

        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        repository.SetupGet(r => r.TournamentRegistrations).Returns(new List<TournamentRegistration>
        {
            new() { Id = 1, CoachLastName = "Alpha", SchoolID = 10, ProblemID = 1, Paid = null },
            new() { Id = 2, CoachLastName = "Beta", SchoolID = 11, ProblemID = 2, Paid = 1 },
            new() { Id = 3, CoachLastName = "Gamma", SchoolID = 12, ProblemID = 3, Paid = 0 }
        });

        repository.Setup(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>())).Returns((int? schoolId) => $"School {schoolId}");
        repository.Setup(r => r.GetProblemNameFromProblemId(It.IsAny<int?>())).Returns((int? problemId) => $"Problem {problemId}");

        return repository;
    }
}
