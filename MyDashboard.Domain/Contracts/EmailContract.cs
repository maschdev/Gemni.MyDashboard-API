using FluentValidator.Validation;
using MyDashboard.Domain.ValueObjects;

namespace MyDashboard.Domain.Contracts
{
    public class EmailContract : IContract
    {
        public ValidationContract Contract { get; }

        public EmailContract(Email email)
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(email.Address, "Address", "Email obrigatório")
                .IsEmail(email.Address, "Address", "Email inválido");
        }

    }
}
