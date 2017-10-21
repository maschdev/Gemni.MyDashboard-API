using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class DeleteCustomerResult : ICommandResult
    {
        public DeleteCustomerResult()
        {

        }

        public DeleteCustomerResult(bool valid)
        {
            Valid = valid;
        }

        public bool Valid { get; set; }
    }
}
