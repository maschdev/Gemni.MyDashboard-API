using MyDashboard.Shared.Commands;
using System;

namespace MyDashboard.Domain.Commands.Results
{
    public class GetCustomerResult : ICommandResult
    {
        public GetCustomerResult()
        {

        }

        //public GetCustomerResult( Guid id, string firstName, string lastName, string email, string document, string username)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Document = document;
        //    Username = username;
        //}

        public Guid   Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Username { get; set; }
    }
}
