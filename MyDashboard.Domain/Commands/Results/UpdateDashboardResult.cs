using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Results
{
    public class UpdateDashboardResult : ICommandResult
    {
        public UpdateDashboardResult()
        {

        }

        public UpdateDashboardResult(bool valid)
        {
            Valid = valid;
        }


        public bool Valid { get; set; }
    }
}
