using AtWork.Domain.Application.Funcionario.Commands;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Structs.Messages;
using FluentValidation;

namespace AtWork.Domain.Application.Funcionario.Validators
{
    public class CreateFuncionarioValidator : AbstractValidator<CreateFuncionarioCommand>
    {
        public CreateFuncionarioValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(item => item.Nome)
                .NotEmpty().WithMessage(MessagesStruct.NOME_EH_OBRIGATORIO);

            RuleFor(item => item.Login)
                .NotEmpty().WithMessage(MessagesStruct.EMAIL_EH_OBRIGATORIO);

            RuleFor(item => item.Senha)
                .NotEmpty().WithMessage(MessagesStruct.SENHA_EH_OBRIGATORIO);

            RuleFor(item => item.ConfirmarSenha)
                .NotEmpty().WithMessage(MessagesStruct.SENHA_DE_CONFIRMACAO_EH_OBRIGATORIO);

            RuleFor(item => item.Email)
                .NotEmpty().WithMessage(MessagesStruct.EMAIL_EH_OBRIGATORIO)
                .EmailAddress().WithMessage(MessagesStruct.EMAIL_INFORMADO_NAO_EH_VALIDO);

            RuleFor(item => item).CustomAsync(async (values, context, ct) =>
            {
                string email = values.Email.ToLower();
                string senha = values.Senha;
                string confirmSenha = values.ConfirmarSenha;
                string login = values.Login;

                TB_Funcionario? funcDuplicado = await unitOfWork.Repository.GetAsync<TB_Funcionario>(item => item.Login == login || (item.Login == login && item.Email.ToLower() == email), ct);
                if (funcDuplicado is not null)
                {
                    bool emailDuplicado = funcDuplicado.Email.ToLower() == email;
                    bool loginDuplicado = funcDuplicado.Login == login;

                    if (emailDuplicado)
                    {
                        context.AddFailure(MessagesStruct.EMAIL_JA_CADASTRADO);
                    }

                    if (loginDuplicado)
                    {
                        context.AddFailure(MessagesStruct.FUNCIONARIO_JA_CADASTRADO);
                    }
                }

                bool senhasIguais = senha == confirmSenha;
                if (!senhasIguais)
                {
                    context.AddFailure(MessagesStruct.SENHA_DEVEM_SER_IGUAIS);
                }
            });
        }
    }
}
