using MyDashboard.Domain.ValueObjects;
using MyDashboard.Shared.Entities;
using System;
using System.Text;

namespace MyDashboard.Domain.Entities
{
    public class Customer : Entity
    {
        protected Customer() { }

        public Customer(Name name, Email email, User user, Document document, string phone)
        {
            Name = name;
            Email = email;
            Document = document;
            User = user;
            Phone = phone;
        }

        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public string Phone { get; private set; }
        public bool Enable { get; private set; }

        public void Active() => Enable = true;

        public void Deactivate() => Enable = false;

        public void Update(Name name, Email email, User user, Document document, string phone)
        {
            Name.Update(name.FirstName, name.LastName);
            Email.Udpate(email.Address);
            Document.Update(document.Number);
            User.Update(user.Name);
            Phone = phone;
        }

        public bool Authenticate(string email, string password)
        {
            if (Email.Address.Trim() == email.Trim() && User.Password.Trim() == EncryptPassword(password).Trim())
                return true;

            AddNotification("Password", "Usuário ou senha inválidos");
            return false;
        }

        private static string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));

            return sbString.ToString();
        }

    }
}
