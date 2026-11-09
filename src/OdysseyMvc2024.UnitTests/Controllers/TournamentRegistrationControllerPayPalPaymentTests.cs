// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPayPalPaymentTests.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Tests for the standalone tournament registration PayPal payment page.
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

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPayPalPaymentTests
{
    [Fact]
    public void PayPalPaymentGet_WhenPayPalIsEnabled_ReturnsViewWithEmptyRequiredFields()
    {
        // Arrange
        var repository = CreateRepository(acceptingPayPal: "true");
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.PayPalPayment();

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<PayPalPaymentViewData>().Subject;

        model.AcceptingPayPal.Should().BeTrue();
        model.TeamTournamentId.Should().BeEmpty();
        model.CoachLastName.Should().BeEmpty();
    }

    [Fact]
    public void PayPalPaymentGet_WhenPayPalIsDisabled_ReturnsViewWithPayPalDisabledModel()
    {
        // Arrange
        var repository = CreateRepository(acceptingPayPal: "false");
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.PayPalPayment();

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<PayPalPaymentViewData>().Subject;

        model.AcceptingPayPal.Should().BeFalse();
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

    private static Mock<IOdysseyRepository> CreateRepository(string acceptingPayPal)
    {
        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(new Dictionary<string, string>
        {
            ["HomePage"] = "https://www.novanorth.org",
            ["AcceptingPayPal"] = acceptingPayPal
        });

        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        return repository;
    }
}
