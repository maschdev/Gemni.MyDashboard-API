using FluentValidator.Validation;
using MyDashboard.Domain.ValueObjects;

namespace MyDashboard.Domain.Contracts
{
    public class DocumentContract : IContract
    {
        public ValidationContract Contract { get; }

        public DocumentContract(Document document)
        {

            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(document.Number, "Document", "Documento obrigatório");


            if (document.Number != null)
            {
                switch (document.Number.Length)
                {
                    case 11:
                        if (!IsCpf(document.Number))
                            Contract.AddNotification("Document", "CPF inválido!");
                        break;
                    case 14:
                        if (!IsCnpj(document.Number))
                            Contract.AddNotification("Document", "CNPJ inválido!");
                        break;
                    default:
                        Contract.AddNotification("Document", "CPF/CNPJ inválido!");
                        break;
                }
            }

        }

        private static bool HasLetter(string number) => System.Text.RegularExpressions.Regex.Match(number, @"[a-zA-Z]").Success;

        private static bool IsCnpj(string cnpj)
        {
            if (HasLetter(cnpj))
                return false;

            var multiplicator1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicator2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            var rest = 0;
            var digit = "";
            var tempCnpj = "";
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            tempCnpj = cnpj.Substring(0, 12);

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator1[i];
            }

            rest = (sum % 11);
            rest = rest < 2 ? 0 : rest = 11 - rest;
            digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator2[i];
            }

            rest = (sum % 11);
            rest = rest < 2 ? 0 : rest = 11 - rest;
            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        private static bool IsCpf(string cpf)
        {
            if (HasLetter(cpf))
                return false;

            var multiplicator1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicator2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var tempCpf = "";
            var digit = "";
            var sum = 0;
            var rest = 0;
            cpf = cpf.Replace(".", "").Replace("-", "");

            tempCpf = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator1[i];
            }

            rest = sum % 11;
            rest = rest < 2 ? 0 : rest = 11 - rest;
            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator2[i];
            }

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }
    }
}
