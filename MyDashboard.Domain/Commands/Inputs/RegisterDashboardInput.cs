using MyDashboard.Shared.Commands;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Commands.Inputs
{
    public class RegisterDashboardInput : ICommand
    {
        public RegisterDashboardInput()
        {
            Dashboards = new List<Dashboard>();
        }
        
        public List<Dashboard> Dashboards { get; set; }
        
        public class Dashboard
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public string Url { get; set; }
            public Guid UserId { get; set; }
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public Guid UserId { get; set; }
    }
}
