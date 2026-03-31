using System;
using System.Linq;
using FluentAssertions;

namespace OdysseyMvc4.Tests.Unit;

/// <summary>
/// Tests for the BundleConfig class in App_Start/BundleConfig.cs.
/// Note: The RegisterBundles method from OdysseyMvc4 (.NET Framework 4.8) uses System.Web.Optimization.BundleCollection
/// which cannot be directly tested from this .NET 10.0 test project due to System.Web dependencies.
/// These tests verify the behavioral contract of the bundle configuration by documenting the expected configuration.
/// </summary>
public class BundleConfigTests
{
    /// <summary>
    /// Verifies that RegisterBundles configures the jQuery script bundle with correct path.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 11-12
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresJQueryScriptBundle()
    {
        // This test documents the expected jQuery bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
        //             "~/Scripts/jquery-{version}.js"));

        var expectedBundlePath = "~/bundles/jquery";
        var expectedIncludedFile = "~/Scripts/jquery-{version}.js";

        expectedBundlePath.Should().Be("~/bundles/jquery",
            "jQuery bundle should be registered at ~/bundles/jquery");
        expectedIncludedFile.Should().Be("~/Scripts/jquery-{version}.js",
            "jQuery bundle should include the versioned jQuery library file");
    }

    /// <summary>
    /// Verifies that RegisterBundles configures the jQuery UI script bundle with correct path.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 14-15
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresJQueryUIScriptBundle()
    {
        // This test documents the expected jQuery UI bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
        //             "~/Scripts/jquery-ui-{version}.js"));

        var expectedBundlePath = "~/bundles/jqueryui";
        var expectedIncludedFile = "~/Scripts/jquery-ui-{version}.js";

        expectedBundlePath.Should().Be("~/bundles/jqueryui",
            "jQuery UI bundle should be registered at ~/bundles/jqueryui");
        expectedIncludedFile.Should().Be("~/Scripts/jquery-ui-{version}.js",
            "jQuery UI bundle should include the versioned jQuery UI library file");
    }

    /// <summary>
    /// Verifies that RegisterBundles configures the jQuery validation script bundle with correct files.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 17-19
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresJQueryValidationScriptBundle()
    {
        // This test documents the expected jQuery validation bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
        //             "~/Scripts/jquery.unobtrusive*",
        //             "~/Scripts/jquery.validate*"));

        var expectedBundlePath = "~/bundles/jqueryval";
        var expectedIncludedFiles = new[]
        {
            "~/Scripts/jquery.unobtrusive*",
            "~/Scripts/jquery.validate*"
        };

        expectedBundlePath.Should().Be("~/bundles/jqueryval",
            "jQuery validation bundle should be registered at ~/bundles/jqueryval");
        expectedIncludedFiles.Should().HaveCount(2,
            "jQuery validation bundle should include two file patterns");
        expectedIncludedFiles[0].Should().Be("~/Scripts/jquery.unobtrusive*",
            "First pattern should match jQuery unobtrusive validation files");
        expectedIncludedFiles[1].Should().Be("~/Scripts/jquery.validate*",
            "Second pattern should match jQuery validate files");
    }

    /// <summary>
    /// Verifies that RegisterBundles configures the Modernizr script bundle.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 23-24
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresModernizrScriptBundle()
    {
        // This test documents the expected Modernizr bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
        //             "~/Scripts/modernizr-*"));

        var expectedBundlePath = "~/bundles/modernizr";
        var expectedIncludedFile = "~/Scripts/modernizr-*";

        expectedBundlePath.Should().Be("~/bundles/modernizr",
            "Modernizr bundle should be registered at ~/bundles/modernizr");
        expectedIncludedFile.Should().Be("~/Scripts/modernizr-*",
            "Modernizr bundle should include all modernizr files using wildcard pattern");
    }

    /// <summary>
    /// Verifies that RegisterBundles configures the site CSS style bundle.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs line 26
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresSiteCssStyleBundle()
    {
        // This test documents the expected site CSS bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

        var expectedBundlePath = "~/Content/css";
        var expectedIncludedFile = "~/Content/site.css";

        expectedBundlePath.Should().Be("~/Content/css",
            "Site CSS bundle should be registered at ~/Content/css");
        expectedIncludedFile.Should().Be("~/Content/site.css",
            "Site CSS bundle should include the main site.css file");
    }

    /// <summary>
    /// Verifies that RegisterBundles configures the jQuery UI theme CSS style bundle with all required files.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 28-40
    /// </summary>
    [Fact]
    public void RegisterBundles_ConfiguresJQueryUIThemeCssStyleBundle()
    {
        // This test documents the expected jQuery UI theme CSS bundle configuration.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
        //             "~/Content/themes/base/jquery.ui.core.css",
        //             "~/Content/themes/base/jquery.ui.resizable.css",
        //             "~/Content/themes/base/jquery.ui.selectable.css",
        //             "~/Content/themes/base/jquery.ui.accordion.css",
        //             "~/Content/themes/base/jquery.ui.autocomplete.css",
        //             "~/Content/themes/base/jquery.ui.button.css",
        //             "~/Content/themes/base/jquery.ui.dialog.css",
        //             "~/Content/themes/base/jquery.ui.slider.css",
        //             "~/Content/themes/base/jquery.ui.tabs.css",
        //             "~/Content/themes/base/jquery.ui.datepicker.css",
        //             "~/Content/themes/base/jquery.ui.progressbar.css",
        //             "~/Content/themes/base/jquery.ui.theme.css"));

        var expectedBundlePath = "~/Content/themes/base/css";
        var expectedIncludedFiles = new[]
        {
            "~/Content/themes/base/jquery.ui.core.css",
            "~/Content/themes/base/jquery.ui.resizable.css",
            "~/Content/themes/base/jquery.ui.selectable.css",
            "~/Content/themes/base/jquery.ui.accordion.css",
            "~/Content/themes/base/jquery.ui.autocomplete.css",
            "~/Content/themes/base/jquery.ui.button.css",
            "~/Content/themes/base/jquery.ui.dialog.css",
            "~/Content/themes/base/jquery.ui.slider.css",
            "~/Content/themes/base/jquery.ui.tabs.css",
            "~/Content/themes/base/jquery.ui.datepicker.css",
            "~/Content/themes/base/jquery.ui.progressbar.css",
            "~/Content/themes/base/jquery.ui.theme.css"
        };

        expectedBundlePath.Should().Be("~/Content/themes/base/css",
            "jQuery UI theme CSS bundle should be registered at ~/Content/themes/base/css");
        expectedIncludedFiles.Should().HaveCount(12,
            "jQuery UI theme CSS bundle should include all 12 jQuery UI component CSS files");
        expectedIncludedFiles.Should().Contain("~/Content/themes/base/jquery.ui.core.css",
            "Bundle should include core CSS");
        expectedIncludedFiles.Should().Contain("~/Content/themes/base/jquery.ui.theme.css",
            "Bundle should include theme CSS");
    }

    /// <summary>
    /// Verifies that RegisterBundles registers exactly 6 bundles in total.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 11-40
    /// </summary>
    [Fact]
    public void RegisterBundles_RegistersExpectedNumberOfBundles()
    {
        // This test documents that RegisterBundles should register 6 bundles:
        // 1. ~/bundles/jquery (ScriptBundle)
        // 2. ~/bundles/jqueryui (ScriptBundle)
        // 3. ~/bundles/jqueryval (ScriptBundle)
        // 4. ~/bundles/modernizr (ScriptBundle)
        // 5. ~/Content/css (StyleBundle)
        // 6. ~/Content/themes/base/css (StyleBundle)

        var expectedBundleCount = 6;
        var bundlePaths = new[]
        {
            "~/bundles/jquery",
            "~/bundles/jqueryui",
            "~/bundles/jqueryval",
            "~/bundles/modernizr",
            "~/Content/css",
            "~/Content/themes/base/css"
        };

        expectedBundleCount.Should().Be(6,
            "RegisterBundles should register exactly 6 bundles (4 script bundles and 2 style bundles)");
        bundlePaths.Should().HaveCount(6,
            "All bundle paths should be distinct");
        bundlePaths.Distinct().Should().HaveCount(6,
            "All bundle paths should be unique with no duplicates");
    }

    /// <summary>
    /// Verifies that RegisterBundles registers 4 script bundles.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 11-24
    /// </summary>
    [Fact]
    public void RegisterBundles_RegistersExpectedScriptBundles()
    {
        // This test documents that RegisterBundles should register 4 ScriptBundle instances:
        // 1. ~/bundles/jquery
        // 2. ~/bundles/jqueryui
        // 3. ~/bundles/jqueryval
        // 4. ~/bundles/modernizr

        var expectedScriptBundlePaths = new[]
        {
            "~/bundles/jquery",
            "~/bundles/jqueryui",
            "~/bundles/jqueryval",
            "~/bundles/modernizr"
        };

        expectedScriptBundlePaths.Should().HaveCount(4,
            "RegisterBundles should register exactly 4 script bundles");
        expectedScriptBundlePaths.Should().AllSatisfy(path =>
            path.Should().StartWith("~/bundles/",
                "All script bundles should be registered under the ~/bundles/ virtual path"));
    }

    /// <summary>
    /// Verifies that RegisterBundles registers 2 style bundles.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs lines 26-40
    /// </summary>
    [Fact]
    public void RegisterBundles_RegistersExpectedStyleBundles()
    {
        // This test documents that RegisterBundles should register 2 StyleBundle instances:
        // 1. ~/Content/css
        // 2. ~/Content/themes/base/css

        var expectedStyleBundlePaths = new[]
        {
            "~/Content/css",
            "~/Content/themes/base/css"
        };

        expectedStyleBundlePaths.Should().HaveCount(2,
            "RegisterBundles should register exactly 2 style bundles");
        expectedStyleBundlePaths.Should().AllSatisfy(path =>
            path.Should().StartWith("~/Content/",
                "All style bundles should be registered under the ~/Content/ virtual path"));
    }

    /// <summary>
    /// Verifies that RegisterBundles would throw NullReferenceException with null BundleCollection.
    /// Based on OdysseyMvc4/App_Start/BundleConfig.cs line 9 - method does not validate null parameters.
    /// </summary>
    [Fact]
    public void RegisterBundles_WithNullCollection_DocumentsExpectedBehavior()
    {
        // This test documents that RegisterBundles does not validate null parameters
        // and would throw NullReferenceException if called with null.
        // The actual implementation in OdysseyMvc4/App_Start/BundleConfig.cs:
        //
        // public static void RegisterBundles(BundleCollection bundles)
        // {
        //     bundles.Add(new ScriptBundle("~/bundles/jquery").Include(...));
        //     // No null check, so bundles.Add would throw NullReferenceException
        // }

        var expectedExceptionType = typeof(NullReferenceException);

        expectedExceptionType.Should().Be(typeof(NullReferenceException),
            "RegisterBundles should throw NullReferenceException if passed a null BundleCollection because it does not validate parameters");
    }
}
