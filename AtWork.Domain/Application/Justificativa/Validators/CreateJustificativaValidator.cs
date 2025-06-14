using AtWork.Domain.Application.Justificativa.Commands;
using AtWork.Shared.Converters;
using AtWork.Shared.Extensions;
using AtWork.Shared.Structs.Messages;
using FluentValidation;

namespace AtWork.Domain.Application.Justificativa.Validators
{
    public class CreateJustificativaValidator : AbstractValidator<CreateJustificativaCommand>
    {
        private readonly static string[] tipo_images = ["image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp", "image/jpg"];

        public CreateJustificativaValidator()
        {
            RuleFor(x => x).Custom((values, context) =>
            {
                DateTime hoje = DateTime.UtcNow.ToBrazilianTime();
                DateTime dt_justificativa = values.DT_Justificativa.ToBrazilianTime();

                if (dt_justificativa.Month != hoje.Month || dt_justificativa.Year != hoje.Year)
                {
                    context.AddFailure(MessagesStruct.JUSTIFICATIVA_PRECISA_SER_DESTE_MES);
                }

                if (values.ImagemJustificativa is not null && values.ImagemJustificativa.Length > 0)
                {
                    string? contentType = B64_Converter.GetMimeTypeFromBase64(values.ImagemJustificativa);
                    if (contentType is null || !tipo_images.Contains(contentType))
                    {
                        context.AddFailure(MessagesStruct.IMAGEM_INFORMADA_NAO_EH_VALIDA);
                    };
                }
            });
        }
    }
}
