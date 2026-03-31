using System;
using System.Linq;
using System.Web.Http;
using FluentAssertions;

namespace OdysseyMvc4.Tests.Unit;

/// <summary>
/// Tests for the WebApiConfig class in App_Start/WebApiConfig.cs.
/// </summary>
public class WebApiConfigTests
{
    [Fact]
    public void Register_WithValidConfiguration_AddsDefaultApiRoute()
    {
        // Arrange
        var config = new HttpConfiguration();

        // Act
        WebApiConfig.Register(config);

        // Assert
        config.Routes.Should().HaveCount(1, "Register should add exactly one route");
        var route = config.Routes.Single();
        route.RouteTemplate.Should().Be("api/{controller}/{id}", "the route template should follow the default Web API pattern");
    }

    [Fact]
    public void Register_WithValidConfiguration_AddsRouteWithCorrectName()
    {
        // Arrange
        var config = new HttpConfiguration();

        // Act
        WebApiConfig.Register(config);

        // Assert
        var route = config.Routes["DefaultApi"];
        route.Should().NotBeNull("the route should be registered with the name 'DefaultApi'");
        route!.RouteTemplate.Should().Be("api/{controller}/{id}");
    }

    [Fact]
    public void Register_WithValidConfiguration_AddsRouteWithOptionalIdParameter()
    {
        // Arrange
        var config = new HttpConfiguration();

        // Act
        WebApiConfig.Register(config);

        // Assert
        var route = config.Routes.Single();
        var httpRoute = route as System.Web.Http.Routing.IHttpRoute;
        httpRoute.Should().NotBeNull();
        httpRoute!.Defaults.Should().ContainKey("id", "the route should have an 'id' default parameter");
        httpRoute.Defaults["id"].Should().Be(System.Web.Http.RouteParameter.Optional, "the 'id' parameter should be marked as optional");
    }

    [Fact]
    public void Register_WithNullConfiguration_ThrowsNullReferenceException()
    {
        // Arrange
        HttpConfiguration? config = null;

        // Act
        Action act = () => WebApiConfig.Register(config!);

        // Assert
        act.Should().Throw<NullReferenceException>("the method does not validate null parameters");
    }

    [Fact]
    public void Register_WithExistingRoutes_AddsDefaultApiRouteToCollection()
    {
        // Arrange
        var config = new HttpConfiguration();
        config.Routes.MapHttpRoute(
            name: "ExistingRoute",
            routeTemplate: "test/{controller}",
            defaults: new { controller = "Home" }
        );

        // Act
        WebApiConfig.Register(config);

        // Assert
        config.Routes.Should().HaveCount(2, "Register should add one more route to the existing collection");
        config.Routes["ExistingRoute"].Should().NotBeNull("the existing route should still be present");
        config.Routes["DefaultApi"].Should().NotBeNull("the new DefaultApi route should be added");
    }

    [Fact]
    public void Register_WithValidConfiguration_RouteTemplateContainsControllerPlaceholder()
    {
        // Arrange
        var config = new HttpConfiguration();

        // Act
        WebApiConfig.Register(config);

        // Assert
        var route = config.Routes.Single();
        route.RouteTemplate.Should().Contain("{controller}", "the route template should contain a controller placeholder");
    }

    [Fact]
    public void Register_WithValidConfiguration_RouteTemplateContainsIdPlaceholder()
    {
        // Arrange
        var config = new HttpConfiguration();

        // Act
        WebApiConfig.Register(config);

        // Assert
        var route = config.Routes.Single();
        route.RouteTemplate.Should().Contain("{id}", "the route template should contain an id placeholder");
    }
}
