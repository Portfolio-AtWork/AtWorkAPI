using AtWork.Domain.Application.Ponto.Commands;
using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Structs.Messages;
using FluentValidation;

namespace AtWork.Domain.Application.Ponto.Validators
{
    public class CreatePontoManualValidator : AbstractValidator<CreatePontoManualCommand>
    {
        public CreatePontoManualValidator(UserInfo userInfo, IUnitOfWork unitOfWork)
        {
            bool ehUsuario = Guid.Empty != userInfo.ID_Usuario;
            bool ehFuncionario = Guid.Empty != userInfo.ID_Funcionario;

            RuleFor(x => x).CustomAsync(async (values, context, ct) =>
            {
                if (ehUsuario)
                {
                    if (values.ID_Funcionario is null || values.ID_Funcionario == Guid.Empty)
                    {
                        context.AddFailure(MessagesStruct.DEVE_INFORMAR_QUAL_FUNCIONARIO);
                    }
                    else
                    {
                        TB_Funcionario? func = await unitOfWork.Repository.GetAsync<TB_Funcionario>(item => item.ID == values.ID_Funcionario.Value, ct);
                        if (func is null)
                        {
                            context.AddFailure(MessagesStruct.FUNCIONARIO_INFORMADO_NAO_EXISTE);
                        }
                    }
                }
                else if (ehFuncionario && !(values.ID_Funcionario ?? Guid.Empty).Equals(Guid.Empty))
                {
                    context.AddFailure(MessagesStruct.NAO_DEVE_INFORMAR_QUAL_FUNCIONARIO);
                }

                bool anoDiferente = values.DT_Ponto.Year != DateTime.Today.Year;
                if (anoDiferente)
                {
                    context.AddFailure(MessagesStruct.ANO_NAO_PODE_SER_DIFERENTE_DO_ATUAL);
                    return;
                }

                bool mesDiferente = values.DT_Ponto.Month != DateTime.Today.Month;
                if (mesDiferente)
                {
                    context.AddFailure(MessagesStruct.MES_NAO_PODE_SER_DIFERENTE_DO_ATUAL);
                    return;
                }

                bool diaMaiorQueHoje = values.DT_Ponto.Day > DateTime.Today.Day;
                if (diaMaiorQueHoje)
                {
                    context.AddFailure(MessagesStruct.DIA_NAO_PODE_SER_MAIOR_QUE_HOJE);
                    return;
                }
            });
        }
    }
}
