using MyDashboard.Shared.Commands;
using System;

namespace MyDashboard.Domain.Commands.Inputs
{
    public class GetUserDashboardInfoInput : ICommand
    {
        public Guid Id { get; set; }

    }
}
