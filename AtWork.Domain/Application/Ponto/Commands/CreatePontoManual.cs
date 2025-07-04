﻿using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Application.Ponto;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CreatePontoManualCommand(Guid? ID_Funcionario, DateTime DT_Ponto) : IRequest<ObjectResponse<bool>>;

    public class CreatePontoManualHandler(
        IUnitOfWork unitOfWork,
        UserInfo userInfo,
        IBaseValidator<CreatePontoManualCommand, bool> validator,
        DatabaseContext db,
        ISincronizaEntradaSaidaService sincronizaEntradaSaidaService
        ) : IRequestHandler<CreatePontoManualCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoManualCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new(false);

            if (!await validator.IsValidAsync(command, result, cancellationToken))
            {
                return result;
            }

            unitOfWork.BeginTransaction();

            Guid id_funcionario = command.ID_Funcionario ?? userInfo.ID_Funcionario;

            TB_Ponto ponto = new()
            {
                DT_Ponto = command.DT_Ponto.ToBrazilianTime(),
                ST_Ponto = StatusPonto.PendenteAprovacao,
                ID_Funcionario = id_funcionario,
                TP_Ponto = string.Empty
            };

            await unitOfWork.Repository.AddAsync(ponto, cancellationToken);
            await sincronizaEntradaSaidaService.HandleAsync(command.DT_Ponto, id_funcionario, cancellationToken);

            Exception? error = await unitOfWork.SaveChangesAsync(cancellationToken);

            result.Value = error.Ok();
            return result;
        }
    }
}
