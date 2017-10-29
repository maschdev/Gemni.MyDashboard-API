using System;

namespace MyDashboard.Domain.Commands.Results
{
    public class DbUserByFilterResult
    {

        public DbUserByFilterResult()
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }

    }
}
