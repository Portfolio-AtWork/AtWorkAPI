using AtWork.Domain.Application.Justificativa.Commands;
using AtWork.Services.Validator;
using AtWork.Shared.Models;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AtWork.Tests
{
    public class BaseValidatorTests
    {
        private readonly Mock<IValidator<CreateJustificativaCommand>> _validatorMock;
        private readonly BaseValidator<CreateJustificativaCommand, bool> _baseValidator;
        private readonly CancellationToken _ct = CancellationToken.None;

        public BaseValidatorTests()
        {
            _validatorMock = new Mock<IValidator<CreateJustificativaCommand>>();
            _baseValidator = new BaseValidator<CreateJustificativaCommand, bool>(_validatorMock.Object);
        }

        [Fact]
        public async Task IsValidAsync_CommandIsNull_ThrowsException()
        {
            var result = new ObjectResponse<bool>();

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _baseValidator.IsValidAsync(null!, result, _ct));

            Assert.Equal("'command' parameter can not be null", ex.Message);
        }

        [Fact]
        public async Task IsValidAsync_ResultIsNull_ThrowsException()
        {
            var command = CreateMockCommand();

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _baseValidator.IsValidAsync(command, null!, _ct));

            Assert.Equal("'result' parameter can not be null", ex.Message);
        }

        [Fact]
        public async Task IsValidAsync_ValidatorIsNull_ThrowsException()
        {
            var result = new ObjectResponse<bool>();
            var baseValidator = new BaseValidator<CreateJustificativaCommand, bool>(null!);

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                baseValidator.IsValidAsync(CreateMockCommand(), result, _ct));

            Assert.Equal("'IValidator<CreateJustificativaCommand>' is not registered as a service", ex.Message);
        }

        [Fact]
        public async Task IsValidAsync_ValidatorReturnsInvalid_ReturnsFalseAndAddsNotifications()
        {
            var command = CreateMockCommand();
            var result = new ObjectResponse<bool>();

            var errors = new List<ValidationFailure>
            {
                new("Field", "Erro de validação")
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(command, _ct))
                .ReturnsAsync(new ValidationResult(errors));

            var isValid = await _baseValidator.IsValidAsync(command, result, _ct);

            Assert.False(isValid);
            Assert.Single(result.Notifications);
            Assert.Equal("Erro de validação", result.Notifications[0].Message);
        }

        [Fact]
        public async Task IsValidAsync_ValidatorReturnsValid_ReturnsTrue()
        {
            var command = CreateMockCommand();
            var result = new ObjectResponse<bool>();

            _validatorMock
                .Setup(v => v.ValidateAsync(command, _ct))
                .ReturnsAsync(new ValidationResult());

            var isValid = await _baseValidator.IsValidAsync(command, result, _ct);

            Assert.True(isValid);
            Assert.Empty(result.Notifications);
        }

        // Função auxiliar para mockar o comando
        private static CreateJustificativaCommand CreateMockCommand()
        {
            return new CreateJustificativaCommand(
                DT_Justificativa: DateTime.UtcNow,
                ImagemJustificativa: null,
                Justificativa: "Motivo qualquer"
            );
        }
    }
}
