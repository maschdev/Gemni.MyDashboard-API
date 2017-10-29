using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class GetCustomerResult : ICommandResult
    {
        public GetCustomerResult()
        {

        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
    }
}
