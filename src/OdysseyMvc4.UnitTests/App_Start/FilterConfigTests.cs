using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Moq;

namespace OdysseyMvc4.UnitTests;

/// <summary>
/// Tests for the FilterConfig class in App_Start/FilterConfig.cs.
/// </summary>
public class FilterConfigTests
{
    [Fact]
    public void RegisterGlobalFilters_WithValidCollection_AddsHandleErrorAttribute()
    {
        // Arrange
        var filters = new GlobalFilterCollection();

        // Act
        FilterConfig.RegisterGlobalFilters(filters);

        // Assert
        filters.Should().HaveCount(1, "RegisterGlobalFilters should add exactly one filter");
        var filter = filters.Single();
        filter.Instance.Should().BeOfType<HandleErrorAttribute>("the filter should be a HandleErrorAttribute");
    }

    [Fact]
    public void RegisterGlobalFilters_WithNullCollection_ThrowsNullReferenceException()
    {
        // Arrange
        GlobalFilterCollection? filters = null;

        // Act
        Action act = () => FilterConfig.RegisterGlobalFilters(filters!);

        // Assert
        act.Should().Throw<NullReferenceException>("the method does not validate null parameters");
    }

    [Fact]
    public void RegisterGlobalFilters_WithExistingFilters_AddsHandleErrorAttributeToCollection()
    {
        // Arrange
        var filters = new GlobalFilterCollection();
        var existingFilter = new AuthorizeAttribute();
        filters.Add(existingFilter);

        // Act
        FilterConfig.RegisterGlobalFilters(filters);

        // Assert
        filters.Should().HaveCount(2, "RegisterGlobalFilters should add one more filter to the existing collection");
        filters.Should().Contain(f => f.Instance == existingFilter, "the existing filter should still be present");
        filters.Should().Contain(f => f.Instance is HandleErrorAttribute, "the new HandleErrorAttribute should be added");
    }

    [Fact]
    public void RegisterGlobalFilters_AddsHandleErrorAttributeWithDefaultScope()
    {
        // Arrange
        var filters = new GlobalFilterCollection();

        // Act
        FilterConfig.RegisterGlobalFilters(filters);

        // Assert
        var filter = filters.Single();
        filter.Scope.Should().Be(FilterScope.Global, "the filter should be registered at Global scope");
    }

}
