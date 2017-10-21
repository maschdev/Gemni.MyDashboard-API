﻿using FluentValidator;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Contracts;
using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Repositories;
using MyDashboard.Domain.ValueObjects;
using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Handlers
{
    public class CustomerHandler : Notifiable,
        ICommandHandler<RegisterCustomerInput>,
        ICommandHandler<UpdateCustomerInput>,
        ICommandHandler<GetCustomerInput>,
        ICommandHandler<DeleteCustomerInput>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ICommandResult Handle(RegisterCustomerInput command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var user = new User(command.Username, command.Password , command.ConfirmPassword);
            var customer = new Customer(name, email, user, document, command.Phone);

            var nameContract = new NameContract(name);
            var emailContract = new EmailContract(email);
            var documentContract = new DocumentContract(document);
            var userContract = new UserContract(user);
            var customeContract = new CustomerContract(customer, nameContract, emailContract, userContract, documentContract);

            if (customeContract.Contract.Invalid)
                return null;

            if (_customerRepository.DocumentExist(command.Document))
            {
                AddNotification(new Notification("Document", "Cpf/Cnpj já cadastrado"));
                return null;
            }

            customer.Active();
            _customerRepository.Save(customer);

            return new RegisterCustomerResult() { Valid = true, Message = "Cadastro com sucesso" };
        }

        public ICommandResult Handle(UpdateCustomerInput command)
        {
            return new UpdateCustomerResult();
        }

        public ICommandResult Handle(GetCustomerInput command)
        {
            if (command.id == null)
            {
                AddNotification(new Notification("Id","Campo obrigatório"));
                return null;
            }

            var customer = _customerRepository.Get(command.id);

            var result = new GetCustomerResult()
            {
                FirstName = customer.Name.FirstName,
                LastName = customer.Name.LastName,
                Email  = customer.Email.Address,
                Document = customer.Document.Number,
                Username = customer.User.Name
            };

            return result;
        }

        public ICommandResult Handle(DeleteCustomerInput command)
        {
            var customer = _customerRepository.Get(command.Id);

            customer.Deactivate();

            _customerRepository.Update(customer);

            return new DeleteCustomerResult();
        }
    }
}
