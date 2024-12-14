using AtWork.Shared.Models;

namespace AtWork.Domain.Interfaces.Services.Validator
{
    public interface IBaseValidator<TCommand, TResult>
    {
        Task<bool> IsValidAsync(TCommand command, ObjectResponse<TResult> result, CancellationToken ct);
    }
}
