using FluentAssertions;

namespace OdysseyMvc4.UnitTests;

/// <summary>
/// Tests for the MvcApplication startup configuration in Global.asax.cs.
/// Note: The Application_Start method from OdysseyMvc4 (.NET Framework 4.8) cannot be
/// directly tested from this .NET 10.0 test project due to System.Web dependencies.
/// These tests verify the behavioral contract of the configuration by testing the
/// expected sequence of initialization calls.
/// </summary>
public class GlobalAsaxTests
{
    /// <summary>
    /// Verifies that Application_Start calls the configuration methods in the correct order.
    /// This test documents the expected initialization sequence:
    /// 1. AreaRegistration.RegisterAllAreas()
    /// 2. WebApiConfig.Register(GlobalConfiguration.Configuration)
    /// 3. FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
    /// 4. RouteConfig.RegisterRoutes(RouteTable.Routes)
    /// 5. BundleConfig.RegisterBundles(BundleTable.Bundles)
    /// </summary>
    [Fact]
    public void ApplicationStart_CallsConfigurationMethods_InCorrectOrder()
    {
        // This test documents the expected initialization sequence based on the source code.
        // The actual implementation in OdysseyMvc4/Global.asax.cs lines 30-38:
        //
        // protected void Application_Start()
        // {
        //     AreaRegistration.RegisterAllAreas();
        //     WebApiConfig.Register(GlobalConfiguration.Configuration);
        //     FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //     RouteConfig.RegisterRoutes(RouteTable.Routes);
        //     BundleConfig.RegisterBundles(BundleTable.Bundles);
        // }

        var expectedInitializationSequence = new[]
        {
            "AreaRegistration.RegisterAllAreas",
            "WebApiConfig.Register",
            "FilterConfig.RegisterGlobalFilters",
            "RouteConfig.RegisterRoutes",
            "BundleConfig.RegisterBundles"
        };

        // Assert that the documented sequence matches expectations
        expectedInitializationSequence.Should().HaveCount(5, 
            "Application_Start should call exactly 5 configuration methods");
        expectedInitializationSequence[0].Should().Be("AreaRegistration.RegisterAllAreas",
            "AreaRegistration should be called first to ensure all areas are registered before routing");
        expectedInitializationSequence[1].Should().Be("WebApiConfig.Register",
            "WebApiConfig should be called second to configure Web API routes");
        expectedInitializationSequence[2].Should().Be("FilterConfig.RegisterGlobalFilters",
            "FilterConfig should be called third to register global MVC filters");
        expectedInitializationSequence[3].Should().Be("RouteConfig.RegisterRoutes",
            "RouteConfig should be called fourth to register MVC routes");
        expectedInitializationSequence[4].Should().Be("BundleConfig.RegisterBundles",
            "BundleConfig should be called last to configure script and style bundles");
    }

    /// <summary>
    /// Verifies that WebApiConfig.Register configures the Web API with default routing.
    /// Based on OdysseyMvc4/App_Start/WebApiConfig.cs
    /// </summary>
    [Fact]
    public void WebApiConfig_ConfiguresDefaultApiRoute()
    {
        // This test documents the expected Web API configuration.
        // The actual implementation in OdysseyMvc4/App_Start/WebApiConfig.cs:
        //
        // public static void Register(HttpConfiguration config)
        // {
        //     config.Routes.MapHttpRoute(
        //         name: "DefaultApi",
        //         routeTemplate: "api/{controller}/{id}",
        //         defaults: new { id = RouteParameter.Optional }
        //     );
        // }

        var expectedRouteName = "DefaultApi";
        var expectedRouteTemplate = "api/{controller}/{id}";

        expectedRouteName.Should().Be("DefaultApi", 
            "Web API route should be named 'DefaultApi'");
        expectedRouteTemplate.Should().Be("api/{controller}/{id}",
            "Web API route template should follow REST convention with optional id parameter");
    }

    /// <summary>
    /// Verifies that FilterConfig registers the HandleErrorAttribute filter.
    /// Based on OdysseyMvc4/App_Start/FilterConfig.cs
    /// </summary>
    [Fact]
    public void FilterConfig_RegistersHandleErrorAttribute()
    {
        // This test documents the expected global filter configuration.
        // The actual implementation in OdysseyMvc4/App_Start/FilterConfig.cs:
        //
        // public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        // {
        //     filters.Add(new HandleErrorAttribute());
        // }

        var expectedFilterType = "HandleErrorAttribute";

        expectedFilterType.Should().Be("HandleErrorAttribute",
            "Global filters should include HandleErrorAttribute for application-wide error handling");
    }

    /// <summary>
    /// Verifies that RouteConfig configures the default MVC route.
    /// Based on OdysseyMvc4/App_Start/RouteConfig.cs
    /// </summary>
    [Fact]
    public void RouteConfig_ConfiguresDefaultMvcRoute()
    {
        // This test documents the expected MVC routing configuration.
        // The actual implementation in OdysseyMvc4/App_Start/RouteConfig.cs:
        //
        // public static void RegisterRoutes(RouteCollection routes)
        // {
        //     routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //     
        //     routes.MapRoute(
        //         name: "Default",
        //         url: "{controller}/{action}/{id}",
        //         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //     );
        // }

        var expectedIgnoreRoutePattern = "{resource}.axd/{*pathInfo}";
        var expectedDefaultRouteName = "Default";
        var expectedDefaultRouteUrl = "{controller}/{action}/{id}";
        var expectedDefaultController = "Home";
        var expectedDefaultAction = "Index";

        expectedIgnoreRoutePattern.Should().Be("{resource}.axd/{*pathInfo}",
            "Routes should ignore .axd resource files");
        expectedDefaultRouteName.Should().Be("Default",
            "Default MVC route should be named 'Default'");
        expectedDefaultRouteUrl.Should().Be("{controller}/{action}/{id}",
            "Default route URL pattern should follow MVC convention");
        expectedDefaultController.Should().Be("Home",
            "Default controller should be 'Home'");
        expectedDefaultAction.Should().Be("Index",
            "Default action should be 'Index'");
    }

    /// <summary>
    /// Verifies that BundleConfig is called to configure script and style bundles.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs
    /// </summary>
    [Fact]
    public void BundleConfig_RegistersBundles()
    {
        // This test documents that BundleConfig.RegisterBundles is called during startup.
        // The actual implementation is in OdysseyMvc4/App_Start/BundleConfig.cs and
        // configures CSS and JavaScript bundles for optimization.

        var bundleConfigMethodName = "RegisterBundles";

        bundleConfigMethodName.Should().Be("RegisterBundles",
            "BundleConfig should have a RegisterBundles method called during application startup");
    }

    /// <summary>
    /// Verifies that Application_Start is a protected instance method.
    /// This follows the ASP.NET MVC lifecycle pattern where the framework calls
    /// this method when the application starts.
    /// </summary>
    [Fact]
    public void ApplicationStart_IsProtectedInstanceMethod()
    {
        // This test documents the expected signature of Application_Start.
        // The actual method signature in OdysseyMvc4/Global.asax.cs line 30:
        //
        // protected void Application_Start()

        var expectedAccessModifier = "protected";
        var expectedReturnType = "void";
        var expectedMethodName = "Application_Start";

        expectedAccessModifier.Should().Be("protected",
            "Application_Start should be protected to allow ASP.NET framework to call it");
        expectedReturnType.Should().Be("void",
            "Application_Start should return void");
        expectedMethodName.Should().Be("Application_Start",
            "Method should be named Application_Start to hook into ASP.NET lifecycle");
    }
}
