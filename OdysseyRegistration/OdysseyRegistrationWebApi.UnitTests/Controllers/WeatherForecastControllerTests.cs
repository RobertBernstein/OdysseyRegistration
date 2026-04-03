using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OdysseyRegistrationWebApi.Controllers;

namespace OdysseyRegistrationWebApi.UnitTests.Controllers
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Constructor_ValidLogger_SetsLogger()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();

            // Act
            var controller = new WeatherForecastController(mockLogger.Object);

            // Assert
            controller.Should().NotBeNull();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsFiveWeatherForecasts()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsWeatherForecastsWithDates()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get().ToList();

            // Assert
            result.Should().OnlyContain(forecast => forecast.Date > DateOnly.FromDateTime(DateTime.Now));
        }

        [Fact]
        public void Get_WhenCalled_ReturnsWeatherForecastsWithTemperatures()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get().ToList();

            // Assert
            result.Should().OnlyContain(forecast => forecast.TemperatureC >= -20 && forecast.TemperatureC < 55);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsWeatherForecastsWithSummaries()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);
            var validSummaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            // Act
            var result = controller.Get().ToList();

            // Assert
            result.Should().OnlyContain(forecast => validSummaries.Contains(forecast.Summary));
        }

        [Fact]
        public void Get_WhenCalled_ReturnsWeatherForecastsWithIncrementalDates()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var result = controller.Get().ToList();

            // Assert
            result.Should().HaveCount(5);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Date.Should().Be(today.AddDays(i + 1));
            }
        }

        [Fact]
        public void Get_CalledMultipleTimes_ReturnsDifferentResults()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result1 = controller.Get().ToList();
            var result2 = controller.Get().ToList();

            // Assert
            // Due to Random.Shared, temperatures and summaries should vary
            // (extremely low chance that both calls produce identical results)
            bool hasDifference = false;
            for (int i = 0; i < result1.Count; i++)
            {
                if (result1[i].TemperatureC != result2[i].TemperatureC || result1[i].Summary != result2[i].Summary)
                {
                    hasDifference = true;
                    break;
                }
            }

            hasDifference.Should().BeTrue("multiple calls should produce different random values");
        }
    }
}
