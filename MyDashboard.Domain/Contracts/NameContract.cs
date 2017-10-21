using FluentValidator.Validation;
using MyDashboard.Domain.ValueObjects;

namespace MyDashboard.Domain.Contracts
{
    public class NameContract : IContract
    {
        public ValidationContract Contract { get; }

        public NameContract(Name name)
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(name.FirstName, "FirstName", "Nome Obrigatório")
                .HasMinLen(name.FirstName, 3, "FirstName", "Tamanho do Nome inválido!")
                .HasMaxLen(name.FirstName, 60, "FirstName", "Tamanho do Nome inválido!")
                .HasMaxLen(name.LastName, 60, "FirstName", "Tamanho do Sobrenome inválido!");
        }
    }
}
