using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class DeleteDashboardResult : ICommandResult
    {
        public DeleteDashboardResult()
        {

        }

        public DeleteDashboardResult(bool valid)
        {
            Valid = valid;
        }

        public bool Valid { get; set; }
    }
}
