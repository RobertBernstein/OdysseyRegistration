// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPayPalIpnTests.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Tests for PayPal Instant Payment Notification (IPN) processing in tournament registration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPayPalIpnTests
{
    [Fact]
    public async Task PayPalIpn_WhenNotificationIsVerifiedAndCompleted_UpdatesPaidStatus()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object, "VERIFIED", new Dictionary<string, string>
        {
            ["payment_status"] = "Completed",
            ["custom"] = "10"
        });

        // Act
        var result = await controller.PayPalIpn(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        repository.Verify(r => r.UpdateTournamentRegistrationPaidStatus(10, 1), Times.Once);
    }

    [Fact]
    public async Task PayPalIpn_WhenNotificationIsNotVerified_DoesNotUpdatePaidStatus()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object, "INVALID", new Dictionary<string, string>
        {
            ["payment_status"] = "Completed",
            ["custom"] = "10"
        });

        // Act
        var result = await controller.PayPalIpn(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        repository.Verify(r => r.UpdateTournamentRegistrationPaidStatus(It.IsAny<int>(), It.IsAny<short>()), Times.Never);
    }

    [Fact]
    public async Task PayPalIpn_WhenPaymentIsNotCompleted_DoesNotUpdatePaidStatus()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object, "VERIFIED", new Dictionary<string, string>
        {
            ["payment_status"] = "Pending",
            ["custom"] = "10"
        });

        // Act
        var result = await controller.PayPalIpn(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        repository.Verify(r => r.UpdateTournamentRegistrationPaidStatus(It.IsAny<int>(), It.IsAny<short>()), Times.Never);
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
            ["HomePage"] = "https://www.novanorth.org",
            ["AcceptingPayPal"] = "true"
        });

        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>()))
            .Returns(new TournamentRegistration
            {
                Id = 10,
                Paid = null
            });

        repository.Setup(r => r.UpdateTournamentRegistrationPaidStatus(It.IsAny<int>(), It.IsAny<short>())).Returns(1);

        return repository;
    }

    private static TestTournamentRegistrationController CreateController(
        IOdysseyRepository repository,
        string verificationResponse,
        Dictionary<string, string> formValues)
    {
        var controller = new TestTournamentRegistrationController(repository, verificationResponse);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString("www.novanorth.org");
        httpContext.Features.Set<IFormFeature>(new FormFeature(new FormCollection(formValues.ToDictionary(
            kvp => kvp.Key,
            kvp => new Microsoft.Extensions.Primitives.StringValues(kvp.Value)))));

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

    private sealed class TestTournamentRegistrationController : TournamentRegistrationController
    {
        private readonly string verificationResponse;

        [SetsRequiredMembers]
        public TestTournamentRegistrationController(IOdysseyRepository repository, string verificationResponse)
            : base(repository)
        {
            this.verificationResponse = verificationResponse;
        }

        protected override Task<string> ValidatePayPalIpnAsync(
            IReadOnlyDictionary<string, string> formValues,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(verificationResponse);
        }
    }
}
