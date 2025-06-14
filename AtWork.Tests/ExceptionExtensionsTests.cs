using AtWork.Shared.Extensions;

namespace AtWork.Tests
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void Ok_ShouldReturnTrue_WhenExceptionIsNull()
        {
            // Arrange
            Exception? exception = null;

            // Act
            var result = exception.Ok();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Ok_ShouldReturnFalse_WhenExceptionIsNotNull()
        {
            // Arrange
            Exception exception = new Exception("Something went wrong");

            // Act
            var result = exception.Ok();

            // Assert
            Assert.False(result);
        }
    }
}
