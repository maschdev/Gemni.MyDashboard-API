using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class DbUserDashboardInfoResult : ICommandResult
    {
        public DbUserDashboardInfoResult()
        {

        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
    }
}
