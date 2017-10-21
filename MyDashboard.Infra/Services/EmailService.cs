using MyDashboard.Domain.Services;

namespace MyDashboard.Infra.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string name, string email, string subject, string boby)
        {
            // Sysytem.Net.Email
        }
    }
}
