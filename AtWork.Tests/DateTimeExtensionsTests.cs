using AtWork.Shared.Extensions;

namespace AtWork.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void GetFirstMomentOfDate_ShouldReturnStartOfDay()
        {
            // Arrange
            var date = new DateTime(2024, 10, 15, 13, 45, 0);

            // Act
            var result = date.GetFirstMomentOfDate();

            // Assert
            Assert.Equal(new DateTime(2024, 10, 15, 0, 0, 1, DateTimeKind.Unspecified), result);
        }

        [Fact]
        public void GetFirstMomentOfDateOrDefault_ShouldReturnMinValue_WhenNull()
        {
            // Arrange
            DateTime? date = null;

            // Act
            var result = date.GetFirstMomentOfDateOrDefault();

            // Assert
            Assert.Equal(DateTime.MinValue, result);
        }

        [Fact]
        public void GetFirstMomentOfDateOrDefault_ShouldReturnStartOfDay_WhenNotNull()
        {
            // Arrange
            DateTime? date = new DateTime(2024, 1, 1, 15, 30, 0);

            // Act
            var result = date.GetFirstMomentOfDateOrDefault();

            // Assert
            Assert.Equal(new DateTime(2024, 1, 1, 0, 0, 1, DateTimeKind.Unspecified), result);
        }

        [Fact]
        public void GetLastMomentOfDate_ShouldReturnEndOfDay()
        {
            // Arrange
            var date = new DateTime(2024, 10, 15, 13, 45, 0);

            // Act
            var result = date.GetLastMomentOfDate();

            // Assert
            Assert.Equal(new DateTime(2024, 10, 15, 23, 59, 59, DateTimeKind.Unspecified), result);
        }

        [Fact]
        public void GetLastMomentOfDateOrDefault_ShouldReturnMinValue_WhenNull()
        {
            // Arrange
            DateTime? date = null;

            // Act
            var result = date.GetLastMomentOfDateOrDefault();

            // Assert
            Assert.Equal(DateTime.MinValue, result);
        }

        [Fact]
        public void GetLastMomentOfDateOrDefault_ShouldReturnEndOfDay_WhenNotNull()
        {
            // Arrange
            DateTime? date = new DateTime(2024, 1, 1, 10, 0, 0);

            // Act
            var result = date.GetLastMomentOfDateOrDefault();

            // Assert
            Assert.Equal(new DateTime(2024, 1, 1, 23, 59, 59, DateTimeKind.Unspecified), result);
        }

        [Theory]
        [InlineData("0001-01-01 00:00:00")] // DateTime.MinValue
        [InlineData("9999-12-31 23:59:59.9999999")] // DateTime.MaxValue
        public void HasValue_ShouldReturnFalse_ForMinOrMax(string dateStr)
        {
            // Arrange
            var date = DateTime.Parse(dateStr);

            var d = DateTime.MaxValue;

            // Act
            var result = date.HasValue();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasValue_ShouldReturnTrue_ForValidDate()
        {
            // Arrange
            var date = new DateTime(2022, 5, 1);

            // Act
            var result = date.HasValue();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Now_ShouldReturnCurrentTime_InBrazilTimezone()
        {
            // Act
            var result = DateTimeExtensions.Now();

            // Assert
            var brazilOffset = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time").BaseUtcOffset;
            var expected = DateTime.UtcNow + brazilOffset;

            Assert.Equal((decimal)expected.Hour, (decimal)result.Hour, 0);
        }

        [Fact]
        public void ToBrazilianTime_ShouldConvertUtcToBrazilTime()
        {
            // Arrange
            var utcTime = new DateTime(2024, 6, 14, 15, 0, 0, DateTimeKind.Utc);

            // Act
            var result = utcTime.ToBrazilianTime();

            // Assert
            Assert.Equal(TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")), result);
        }

        [Fact]
        public void ToBrazilianTime_ShouldReturnSameTime_IfAlreadyInBrazilTimeZone()
        {
            // Arrange
            var localTime = new DateTime(2024, 6, 14, 10, 0, 0, DateTimeKind.Local);
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            if (TimeZoneInfo.Local.Id != brazilTimeZone.Id)
            {
                // Skip test if system time zone is not Brazil
                return;
            }

            // Act
            var result = localTime.ToBrazilianTime();

            // Assert
            Assert.Equal(localTime, result);
        }
    }
}
