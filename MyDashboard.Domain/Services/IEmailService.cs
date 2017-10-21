namespace MyDashboard.Domain.Services
{
    public interface IEmailService
    {
        void Send(string name, string email, string subject, string boby);
        //SendGrid: barato
    }
}
