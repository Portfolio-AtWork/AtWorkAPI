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
using System.Text.RegularExpressions;

namespace AtWork.Domain.Application.Justificativa.Commands
{
    public record CreateJustificativaCommand(DateTime DT_Justificativa, string? ImagemJustificativa, string Justificativa) : IRequest<ObjectResponse<bool>>;

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

            byte[]? img = TrataImagem(command.ImagemJustificativa);
            string? contentType = GetMimeTypeFromBase64(command.ImagemJustificativa);

            TB_Justificativa? justificativa = await unitOfWork.Repository.AddAsync(new TB_Justificativa()
            {
                ID_Funcionario = userInfo.ID_Funcionario,
                ImagemJustificativa = img,
                Justificativa = command.Justificativa ?? "",
                DT_Justificativa = command.DT_Justificativa.ToBrazilianTime(),
                ST_Justificativa = StatusJustificativa.PendenteAprovacao,
                ImagemContentType = contentType
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

        private static byte[]? TrataImagem(string? imagemJustificativa)
        {
            if (imagemJustificativa is null || imagemJustificativa.Length == 0)
                return null;

            byte[] fileBytes = Convert.FromBase64String(imagemJustificativa);

            if (fileBytes.Length == 0)
            {
                return null;
            }

            return fileBytes;
        }

        private static string? GetMimeTypeFromBase64(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            var match = Regex.Match(base64String, @"^data:(?<type>[\w/+.-]+);base64,", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["type"].Value : null;
        }
    }
}
