using MyDashboard.Shared.Commands;


namespace MyDashboard.Domain.Commands.Inputs
{
    public class UpdateCustomerInput : ICommand
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
