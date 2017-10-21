using FluentValidator;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Repositories;
using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Handlers
{
    public class AuthenticateUserHandler : Notifiable, ICommandHandler<AuthenticateUserInput>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticateUserHandler(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }


        public ICommandResult Handle(AuthenticateUserInput command)
        {
            return new AuthenticateUserResult();
        }


    }
}
