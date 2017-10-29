using MyDashboard.Shared.Commands;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Commands.Results
{
    public class GetUserResult : ICommandResult
    {
        public GetUserResult()
        {
            Users = new List<UserByFilter>();
        }

        public  List<UserByFilter> Users { get; set; }

        public class UserByFilter
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Document { get; set; }
        }
    }
}
