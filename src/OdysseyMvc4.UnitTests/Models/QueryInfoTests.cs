using System.Collections.Generic;
using FluentAssertions;
using OdysseyMvc4.Models;

namespace OdysseyMvc4.UnitTests.Models;

/// <summary>
/// Tests for QueryInfo model to ensure proper initialization and property behaviors.
/// </summary>
public class QueryInfoTests
{
    [Fact]
    public void Constructor_Initializes_CsFieldMapAsDictionary()
    {
        // Arrange & Act
        var queryInfo = new QueryInfo();

        // Assert
        queryInfo.CsFieldMap.Should().NotBeNull();
        queryInfo.CsFieldMap.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_Initializes_CsFieldMapWithCorrectType()
    {
        // Arrange & Act
        var queryInfo = new QueryInfo();

        // Assert
        queryInfo.CsFieldMap.Should().BeOfType<Dictionary<string, string>>();
    }

    [Fact]
    public void Constructor_AllowsAddingToCsFieldMap()
    {
        // Arrange
        var queryInfo = new QueryInfo();

        // Act
        queryInfo.CsFieldMap.Add("key1", "value1");
        queryInfo.CsFieldMap.Add("key2", "value2");

        // Assert
        queryInfo.CsFieldMap.Should().HaveCount(2);
        queryInfo.CsFieldMap["key1"].Should().Be("value1");
        queryInfo.CsFieldMap["key2"].Should().Be("value2");
    }

    [Fact]
    public void Constructor_InitializesOtherPropertiesToDefault()
    {
        // Arrange & Act
        var queryInfo = new QueryInfo();

        // Assert
        queryInfo.OriginalSql.Should().BeNull();
        queryInfo.TableName.Should().BeNull();
    }
}
