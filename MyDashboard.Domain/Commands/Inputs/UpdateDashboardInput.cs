using MyDashboard.Shared.Commands;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Commands.Inputs
{
    public class UpdateDashboardInput : ICommand
    {
        public IList<Dashboard> Dashboards { get; set; }

        public class Dashboard
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Order { get; set; }
            public string Url { get; set; }
        }
    }
}
