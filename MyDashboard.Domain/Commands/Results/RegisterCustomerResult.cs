using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class RegisterCustomerResult : ICommandResult
    {
        public RegisterCustomerResult()
        {

        }

        public RegisterCustomerResult(bool valid)
        {
            Valid = valid;
        }

        public bool Valid { get; set; }
        public string Message { get; set; }
    }
}
