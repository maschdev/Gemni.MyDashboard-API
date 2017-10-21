using MyDashboard.Shared.Commands;


namespace MyDashboard.Domain.Commands.Inputs
{
    public class AuthenticateUserInput : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
