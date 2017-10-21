using MyDashboard.Shared.Commands;
using System;


namespace MyDashboard.Domain.Commands.Inputs
{
    public class GetCustomerInput : ICommand
    {
        public Guid id { get; set; }
    }
}
