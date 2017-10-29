using FluentValidator;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Repositories;
using MyDashboard.Shared.Commands;
using System.Threading.Tasks;

namespace MyDashboard.Domain.Commands.Handlers
{
    public class UserHandler : Notifiable, ICommandHandler<GetUserInput>, ICommandHandler<GetUserDashboardInfoInput>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(GetUserInput command)
        {
            var result = _userRepository.GetByFilter(command.Document, command.DashboardName, command.UserName);

            var users = new GetUserResult();

            Parallel.ForEach(result, user =>
            {
                users.Users.Add(new GetUserResult.UserByFilter()
                {
                    Id = user.Id.ToString(),
                    Document = user.Document.Trim(),
                    Name = user.Name.Trim()
                });
            });

            return users;
        }

        public ICommandResult Handle(GetUserDashboardInfoInput command)
        {
            var user = _userRepository.GetUserDashboardInfo(command.Id);

            return user;
        }
    }
}
