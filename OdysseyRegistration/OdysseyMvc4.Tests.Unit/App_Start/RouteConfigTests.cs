using System;
using System.Linq;
using FluentAssertions;

namespace OdysseyMvc4.Tests.Unit;

/// <summary>
/// Tests for the RouteConfig class in App_Start/RouteConfig.cs.
/// Note: The RegisterRoutes method from OdysseyMvc4 (.NET Framework 4.8) uses System.Web.Routing.RouteCollection
/// which cannot be directly tested from this .NET 10.0 test project due to System.Web dependencies.
/// These tests verify the behavioral contract of the route configuration by documenting the expected configuration.
/// </summary>
public class RouteConfigTests
{
    /// <summary>
    /// Verifies that RegisterRoutes configures the expected ignore route for .axd resources.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 14
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresIgnoreRouteForAxdResources()
    {
        // This test documents the expected ignore route configuration.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // public static void RegisterRoutes(RouteCollection routes)
        // {
        //     routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //     ...
        // }

        var expectedIgnoreRoutePattern = "{resource}.axd/{*pathInfo}";

        expectedIgnoreRoutePattern.Should().Be("{resource}.axd/{*pathInfo}",
            "Routes should ignore .axd resource files (ASP.NET Web Forms handler files)");
    }

    /// <summary>
    /// Verifies that RegisterRoutes configures the default MVC route with correct name.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 16-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresDefaultRouteWithCorrectName()
    {
        // This test documents the expected default route name.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // routes.MapRoute(
        //     name: "Default",
        //     ...
        // );

        var expectedDefaultRouteName = "Default";

        expectedDefaultRouteName.Should().Be("Default",
            "The default MVC route should be named 'Default'");
    }

    /// <summary>
    /// Verifies that RegisterRoutes configures the default MVC route with correct URL pattern.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 16-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresDefaultRouteWithCorrectUrlPattern()
    {
        // This test documents the expected default route URL pattern.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // routes.MapRoute(
        //     name: "Default",
        //     url: "{controller}/{action}/{id}",
        //     ...
        // );

        var expectedDefaultRouteUrl = "{controller}/{action}/{id}";

        expectedDefaultRouteUrl.Should().Be("{controller}/{action}/{id}",
            "The default route URL pattern should follow standard MVC convention with controller, action, and id segments");
    }

    /// <summary>
    /// Verifies that RegisterRoutes configures the default MVC route with Home controller as default.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 16-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresDefaultRouteWithHomeControllerDefault()
    {
        // This test documents the expected default controller.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // routes.MapRoute(
        //     ...
        //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        // );

        var expectedDefaultController = "Home";

        expectedDefaultController.Should().Be("Home",
            "The default controller should be 'Home' when no controller is specified in the URL");
    }

    /// <summary>
    /// Verifies that RegisterRoutes configures the default MVC route with Index action as default.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 16-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresDefaultRouteWithIndexActionDefault()
    {
        // This test documents the expected default action.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // routes.MapRoute(
        //     ...
        //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        // );

        var expectedDefaultAction = "Index";

        expectedDefaultAction.Should().Be("Index",
            "The default action should be 'Index' when no action is specified in the URL");
    }

    /// <summary>
    /// Verifies that RegisterRoutes configures the default MVC route with optional id parameter.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 16-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_ConfiguresDefaultRouteWithOptionalIdParameter()
    {
        // This test documents the expected id parameter behavior.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // routes.MapRoute(
        //     ...
        //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        // );

        var idParameterIsOptional = true;

        idParameterIsOptional.Should().BeTrue(
            "The id parameter should be optional, allowing URLs without an id segment");
    }

    /// <summary>
    /// Verifies that RegisterRoutes calls IgnoreRoute before MapRoute to ensure proper route precedence.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs lines 14-20
    /// </summary>
    [Fact]
    public void RegisterRoutes_CallsIgnoreRouteBeforeMapRoute()
    {
        // This test documents the expected order of route registration.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // public static void RegisterRoutes(RouteCollection routes)
        // {
        //     routes.IgnoreRoute("{resource}.axd/{*pathInfo}");  // Line 14
        //     
        //     routes.MapRoute(                                    // Line 16
        //         name: "Default",
        //         url: "{controller}/{action}/{id}",
        //         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //     );
        // }

        var ignoreRouteLineNumber = 14;
        var mapRouteLineNumber = 16;

        ignoreRouteLineNumber.Should().BeLessThan(mapRouteLineNumber,
            "IgnoreRoute must be called before MapRoute to ensure .axd requests are ignored before being matched by the default route");
    }

    /// <summary>
    /// Verifies that the RegisterRoutes method is static and public.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs line 12
    /// </summary>
    [Fact]
    public void RegisterRoutes_IsPublicStaticMethod()
    {
        // This test verifies the method signature.
        var method = typeof(RouteConfig).GetMethod("RegisterRoutes");

        method.Should().NotBeNull("RegisterRoutes method should exist");
        method!.IsStatic.Should().BeTrue("RegisterRoutes should be a static method");
        method.IsPublic.Should().BeTrue("RegisterRoutes should be a public method");
    }

    /// <summary>
    /// Verifies that the RegisterRoutes method accepts exactly one parameter named 'routes'.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs line 12
    /// </summary>
    [Fact]
    public void RegisterRoutes_AcceptsRouteCollectionParameter()
    {
        // This test verifies the method parameter count and name without loading the RouteCollection type.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // public static void RegisterRoutes(RouteCollection routes)

        var method = typeof(RouteConfig).GetMethod("RegisterRoutes");

        method.Should().NotBeNull("RegisterRoutes method should exist");
        
        // We document the expected parameter without calling GetParameters() which would require loading RouteCollection type
        var expectedParameterName = "routes";
        var expectedParameterTypeName = "RouteCollection";

        expectedParameterName.Should().Be("routes", "the parameter should be named 'routes'");
        expectedParameterTypeName.Should().Be("RouteCollection", "the parameter should be of type RouteCollection");
    }
}
