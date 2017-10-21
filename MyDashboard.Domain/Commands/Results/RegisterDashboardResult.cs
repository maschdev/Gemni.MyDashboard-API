using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class RegisterDashboardResult : ICommandResult
    {
        public RegisterDashboardResult()
        {

        }

        public RegisterDashboardResult(bool valid, string message)
        {
            Valid = valid;
            Message = message;
        }

        public bool Valid { get; set; }
        public string Message { get; set; }

    }
}
