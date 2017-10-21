using MyDashboard.Domain.Enums;
using MyDashboard.Shared.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDashboard.Domain.Entities
{
    public class User : Entity
    {
        protected User() { }

        private readonly IList<Dashboard> _dashboards;

        public User(string userName, string password, string confirmPassword)
        {
            Name = userName;
            Password = EncryptPassword(password);
            ConfirmPassword = confirmPassword;
            ProfileId = (int)EnumProfile.User;

            _dashboards = new List<Dashboard>();
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
        public int ProfileId { get; private set; }

        public ICollection<Dashboard> Dashboards => _dashboards.ToArray();

        public bool Authenticate(string userName, string password)
        {
            if (Name == userName && Password == EncryptPassword(password))
                return true;

            AddNotification("Password", "Usuário ou senha inválidos");
            return false;
        }


        public void Update(string name) => Name = name;


        //public void Activate() => this.Active = true;
        //public void Deactivate() => this.Active = false;

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
