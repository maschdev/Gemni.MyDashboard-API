using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class AuthenticateUserResult : ICommandResult
    {
        public AuthenticateUserResult()
        {

        }


        public AuthenticateUserResult(bool valid)
        {
            Valid = valid;
        }

        public bool Valid { get; set; }
    }
}
