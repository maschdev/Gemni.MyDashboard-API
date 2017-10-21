using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDashboard.Shared.Commands;

namespace MyDashboard.Domain.Commands.Inputs
{
    public class GetDashboardInput : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
