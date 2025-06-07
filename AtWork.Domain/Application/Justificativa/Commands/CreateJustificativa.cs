using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AtWork.Domain.Application.Justificativa.Commands
{
    public record CreateJustificativaCommand(DateTime DT_Justificativa, IFormFile? ImagemJustificativa, string Justificativa) : IRequest<ObjectResponse<bool>>;

    public class CreateJustificativaHandler(IUnitOfWork unitOfWork, UserInfo userInfo, IBaseValidator<CreateJustificativaCommand, bool> validator) : IRequestHandler<CreateJustificativaCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateJustificativaCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new(false);

            if (!await validator.IsValidAsync(command, result, cancellationToken))
            {
                return result;
            }

            unitOfWork.BeginTransaction();

            byte[]? img = await TrataImagem(command.ImagemJustificativa);

            TB_Justificativa? justificativa = await unitOfWork.Repository.AddAsync(new TB_Justificativa()
            {
                ID_Funcionario = userInfo.ID_Funcionario,
                ImagemJustificativa = img,
                Justificativa = command.Justificativa ?? "",
                DT_Justificativa = command.DT_Justificativa.ToBrazilianTime(),
                ST_Justificativa = StatusJustificativa.PendenteAprovacao
            }, cancellationToken);

            if (justificativa is null)
            {
                unitOfWork.Rollback();
                result.Value = false;
                return result;
            }

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();
            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_SALVAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_SALVAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }

            return result;
        }

        private static async Task<byte[]?> TrataImagem(IFormFile? imagemJustificativa)
        {
            if (imagemJustificativa is null || imagemJustificativa.Length == 0)
                return null;

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await imagemJustificativa.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            if (fileBytes.Length == 0)
            {
                return null;
            }

            return fileBytes;
        }
    }
}
