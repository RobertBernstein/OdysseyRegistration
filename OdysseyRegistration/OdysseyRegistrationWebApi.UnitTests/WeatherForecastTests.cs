using FluentAssertions;
using OdysseyRegistrationWebApi;

namespace OdysseyRegistrationWebApi.UnitTests;

public class WeatherForecastTests
{
    [Fact]
    public void TemperatureF_WithZeroCelsius_ReturnsThirtyTwo()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = 0 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(32);
    }

    [Fact]
    public void TemperatureF_WithPositiveCelsius_ConvertsUsingFormula()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = 100 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(211);
    }

    [Fact]
    public void TemperatureF_WithNegativeCelsius_ConvertsUsingFormula()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = -40 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(-39);
    }

    [Fact]
    public void TemperatureF_WithTwentyCelsius_ReturnsSixtySeven()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = 20 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(67);
    }

    [Fact]
    public void TemperatureF_WithThirtyCelsius_ReturnsEightyFive()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = 30 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(85);
    }

    [Fact]
    public void TemperatureF_WithMinusTenCelsius_ReturnsFifteen()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = -10 };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(15);
    }

    [Fact]
    public void TemperatureF_WithMaxIntValue_HandlesOverflow()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = int.MaxValue };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().Be(-2147483617);
    }

    [Fact]
    public void TemperatureF_WithMinIntValue_HandlesUnderflow()
    {
        // Arrange
        var forecast = new WeatherForecast { TemperatureC = int.MinValue };

        // Act
        var result = forecast.TemperatureF;

        // Assert
        result.Should().BeLessThan(0);
    }
}
