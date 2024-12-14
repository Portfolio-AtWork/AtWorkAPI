using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Shared.Models;
using FluentValidation;

namespace AtWork.Services.Validator
{
    public class BaseValidator<TCommand, TResult>(IValidator<TCommand> validator) : IBaseValidator<TCommand, TResult>
    {
        public async Task<bool> IsValidAsync(TCommand command, ObjectResponse<TResult> result, CancellationToken ct)
        {
            if (command is null)
            {
                throw new Exception("'command' parameter can not be null");
            }

            if (result is null)
            {
                throw new Exception("'result' parameter can not be null");
            }

            if (validator is null)
            {
                string className = typeof(TCommand).Name;
                throw new Exception($"'IValidator<{className}>' is not registered as a service");
            }

            var validation = await validator.ValidateAsync(command, ct);

            if (!validation.IsValid)
            {
                result.AddNotifications(validation.Errors);
                result.Value = default!;
                return false;
            }

            return true;
        }
    }
}
