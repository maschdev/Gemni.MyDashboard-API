using FluentValidator.Validation;
using MyDashboard.Domain.Entities;

namespace MyDashboard.Domain.Contracts
{
    public class CustomerContract : IContract
    {
        public ValidationContract Contract { get; }

        public CustomerContract(
            Customer customer,
            NameContract nameContract,
            EmailContract emailContract,
            UserContract userContract,
            DocumentContract documentContract
            )
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(customer.Phone, "Phone", "Telefone de contato obrigatório");

            Contract.AddNotifications(nameContract.Contract.Notifications);
            Contract.AddNotifications(emailContract.Contract.Notifications);
            Contract.AddNotifications(userContract.Contract.Notifications);
            Contract.AddNotifications(documentContract.Contract.Notifications);
        }
    }
}
