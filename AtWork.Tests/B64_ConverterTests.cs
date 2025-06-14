using AtWork.Shared.Converters;
using System.Text;

namespace AtWork.Tests
{
    public class B64_ConverterTests
    {
        [Fact]
        public void GetBytesFromBase64String_ValidBase64_ReturnsByteArray()
        {
            // Arrange
            var text = "Hello, world!";
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

            // Act
            var result = B64_Converter.GetBytesFromBase64String(base64);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Encoding.UTF8.GetBytes(text), result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetBytesFromBase64String_NullOrEmpty_ReturnsNull(string? input)
        {
            var result = B64_Converter.GetBytesFromBase64String(input);

            Assert.Null(result);
        }

        [Fact]
        public void GetBytesFromBase64String_InvalidBase64_ReturnsNull()
        {
            var invalidBase64 = "not-a-valid-base64!";

            var result = B64_Converter.GetBytesFromBase64String(invalidBase64);

            Assert.Null(result);
        }

        [Fact]
        public void GetMimeTypeFromBase64_ValidDataUri_ReturnsMimeType()
        {
            var base64String = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA";

            var result = B64_Converter.GetMimeTypeFromBase64(base64String);

            Assert.Equal("image/png", result);
        }

        [Fact]
        public void GetMimeTypeFromBase64_InvalidDataUri_ReturnsNull()
        {
            var base64String = "not-a-valid-data-uri";

            var result = B64_Converter.GetMimeTypeFromBase64(base64String);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void GetMimeTypeFromBase64_NullOrWhitespace_ReturnsNull(string? input)
        {
            var result = B64_Converter.GetMimeTypeFromBase64(input);

            Assert.Null(result);
        }
    }
}
