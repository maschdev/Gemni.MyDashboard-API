using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class UpdateCustomerResult : ICommandResult
    {
        public UpdateCustomerResult()
        {

        }

        public UpdateCustomerResult(bool valid)
        {
            Valid = valid;
        }


        public bool Valid { get; set; }
    }
}
