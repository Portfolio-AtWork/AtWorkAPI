using AtWork.Domain.Application.Ponto.Commands;
using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using FluentValidation;
using TimeZoneConverter;

namespace AtWork.Domain.Application.Ponto.Validators
{
    public class CreatePontoManualValidator : AbstractValidator<CreatePontoManualCommand>
    {
        private static readonly TimeZoneInfo SaoPauloTimeZone = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

        public CreatePontoManualValidator(UserInfo userInfo, IUnitOfWork unitOfWork)
        {
            bool ehUsuario = userInfo.TP_Login == TipoLogin.Admin;
            bool ehFuncionario = userInfo.TP_Login == TipoLogin.Funcionario;

            RuleFor(x => x).CustomAsync(async (values, context, ct) =>
            {
                bool foiInformadoFuncionarioNaRequest = values.ID_Funcionario is not null && values.ID_Funcionario != Guid.Empty;

                if (ehUsuario)
                {
                    if (!foiInformadoFuncionarioNaRequest)
                    {
                        context.AddFailure(MessagesStruct.DEVE_INFORMAR_QUAL_FUNCIONARIO);
                    }
                    else
                    {
                        TB_Funcionario? func = await unitOfWork.Repository.GetAsync<TB_Funcionario>(
                            item => item.ID == values.ID_Funcionario!.Value, ct
                        );

                        if (func is null)
                        {
                            context.AddFailure(MessagesStruct.FUNCIONARIO_INFORMADO_NAO_EXISTE);
                        }
                    }
                }
                else if (ehFuncionario && foiInformadoFuncionarioNaRequest)
                {
                    context.AddFailure(MessagesStruct.NAO_DEVE_INFORMAR_QUAL_FUNCIONARIO);
                }

                // Converte DateTime.Today e DT_Ponto para o fuso de São Paulo
                DateTime hojeSaoPaulo = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SaoPauloTimeZone);
                DateTime dtPontoSaoPaulo = values.DT_Ponto.Kind == DateTimeKind.Utc
                    ? TimeZoneInfo.ConvertTimeFromUtc(values.DT_Ponto, SaoPauloTimeZone)
                    : values.DT_Ponto;

                if (dtPontoSaoPaulo.Year != hojeSaoPaulo.Year)
                {
                    context.AddFailure(MessagesStruct.ANO_NAO_PODE_SER_DIFERENTE_DO_ATUAL);
                    return;
                }

                if (dtPontoSaoPaulo.Month != hojeSaoPaulo.Month)
                {
                    context.AddFailure(MessagesStruct.MES_NAO_PODE_SER_DIFERENTE_DO_ATUAL);
                    return;
                }

                if (dtPontoSaoPaulo.Day > hojeSaoPaulo.Day)
                {
                    context.AddFailure(MessagesStruct.DIA_NAO_PODE_SER_MAIOR_QUE_HOJE);
                    return;
                }
            });
        }
    }
}
