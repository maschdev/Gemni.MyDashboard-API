using FluentValidator.Validation;
using MyDashboard.Domain.Entities;
using System.Text;

namespace MyDashboard.Domain.Contracts
{
    public class UserContract : IContract
    {
        public ValidationContract Contract { get; }

        public UserContract(User user)
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(user.Name, "Username", "Usuário obrigatório")
                .HasMaxLen(user.Name, 60, "Username", "Tamanho do usuário inválido!")
                .HasMinLen(user.Name, 3, "Username", "Tamanho do usuário inválido!")
                .IsNotNullOrEmpty(user.Password, "Password", "Senha obrigatório")
                //.AreNotEquals(user.Password, EncryptPassword(user.ConfirmPassword), "Password", "As senhas não coincidem!");
                .AreEquals(user.Password, EncryptPassword(user.ConfirmPassword), "Password", "As senhas não coincidem!");
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
