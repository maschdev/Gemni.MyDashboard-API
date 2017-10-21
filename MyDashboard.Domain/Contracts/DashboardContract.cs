using FluentValidator.Validation;
using MyDashboard.Domain.Entities;

namespace MyDashboard.Domain.Contracts
{
    public class DashboardContract : IContract
    {
        public ValidationContract Contract { get; }

        public DashboardContract(Dashboard dashboard)
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(dashboard.Title, "Title", "Nome do dashboard obrigatório")
                .HasMaxLen(dashboard.Title, 60, "Title", "Tamanho do dashboard inválido!")
                .HasMinLen(dashboard.Title, 3, "Title", "Tamanho do dashboard inválido!")
                .IsNotNullOrEmpty(dashboard.Url, "Url", "Link do dashboard obrigatório")
                .IsUrl(dashboard.Url, "Url", "Link do dashboard inválido!")
                .IsLowerOrEqualsThan(dashboard.Order, 0, "Order", "Número da ordem inválido");
        }
    }
}
