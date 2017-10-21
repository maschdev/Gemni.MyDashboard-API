using FluentValidator;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Repositories;
using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Handlers
{
    public class UserHandler : Notifiable, ICommandHandler<GetUserInput>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(GetUserInput command)
        {
            return new GetUserResult();
        }

    }
}
