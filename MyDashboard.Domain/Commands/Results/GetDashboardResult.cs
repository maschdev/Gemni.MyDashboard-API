using MyDashboard.Shared.Commands;
using System.Collections.Generic;

namespace MyDashboard.Domain.Commands.Results
{
    public class GetDashboardResult : ICommandResult
    {
        public GetDashboardResult()
        {
            Dashboards = new List<Dashboard>();
        }

        public List<Dashboard> Dashboards { get; set; }

        public class Dashboard
        {
            public string Title { get; set; }
            public string Order { get; set; }
            public string Url { get; set; }
            public string UserId { get; set; }
        }

        public string Title { get; set; }
        public string Order { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
    }
}
