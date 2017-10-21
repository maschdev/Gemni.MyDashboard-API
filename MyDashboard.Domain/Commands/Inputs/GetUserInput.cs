using MyDashboard.Shared.Commands;
using System;


namespace MyDashboard.Domain.Commands.Inputs
{
    public class GetUserInput : ICommand
    {
        public Guid Id { get; set; }
        public string Document { get; set; }
        public string DashboardName { get; set; }
        public string UserName { get; set; }
    }
}
