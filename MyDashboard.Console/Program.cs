using FluentValidator;
using MyDashboard.Domain.Commands.Handlers;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Contracts;
using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Repositories;
using MyDashboard.Domain.ValueObjects;
using MyDashboard.Infra.Context;
using MyDashboard.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDashboard.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //CadastroNovoCliente();
            CadastrarDashboards();

            //ValidarAcesso();

            //EncryptPassword("gemniadm");
        }


        public static void CadastroNovoCliente()
        {
            var command = new RegisterCustomerInput()
            {
                FirstName = "Caio",
                LastName = "Almeida",
                Document = "23190093881",
                Email = "caio@email.com",
                Phone = "13997471518",
                Username = "caiorapha",
                Password = "caio123",
                ConfirmPassword = "caio123"
            };

            RegisterCustomerRepository(command);

            command.FirstName = "Rapha";
            command.LastName = "Almeida";
            command.Document = "16219573811";
            command.Email = "rapha@email.com";
            command.Phone = "1136030103";
            command.Username = "raphaguimaraes";
            command.Password = "rapha123";
            command.ConfirmPassword = "rapha23";

            RegisterCustomerRepository(command);

        }

        public static void ValidarAcesso()
        {
            var context = new MyDashboardDataContext();
            var repository = new CustomerRepository(context);
            var customer = repository.GetByUserEmail("caio@caio.com");
        }

        public static void CadastrarDashboards()
        {
            var context = new MyDashboardDataContext();
            var customerRepository = new CustomerRepository(context);
            var dashboardRepository = new DashboardRepository(context);

            var customer = customerRepository.Get(new Guid("DF94D184-0623-436E-B40E-430CDC71185B"));// caiorapha

            var dashboards = new List<RegisterDashboardInput>();
            var dashboard = new RegisterDashboardInput()
            {
                Id = "0CFB690B-2DFD-4B42-B061-3AA29D65A2DC",
                Title = "Dashboard 3",
                Order = 3,
                Url = "https://app.powerbi.com/view?r=eyJrIjoiZDM1MWY3ZGYtMzhmYi00Y2NjLWFiZWQtMWI5NjYyYTdmNzdhIiwidCI6ImFjNTAxNjA3LWJmN2MtNDk2NC04MjY1LWMxMjZmNzg1ZmU4ZSJ9",
                CustomerId = customer.Id.ToString()
            };

            dashboards.Add(dashboard);
            dashboard = new RegisterDashboardInput()
            {
                Id = "CF78E30D-EA70-438B-B1C2-8C539310907E",
                Title = "Dashboard 4",
                Order = 4,
                Url = "https://app.powerbi.com/view?r=eyJrIjoiZDM1MWY3ZGYtMzhmYi00Y2NjLWFiZWQtMWI5NjYyYTdmNzdhIiwidCI6ImFjNTAxNjA3LWJmN2MtNDk2NC04MjY1LWMxMjZmNzg1ZmU4ZSJ9",
                CustomerId = customer.UserId.ToString()
            };

            dashboards.Add(dashboard);

            IList<Dashboard> dashboardsToSave = new List<Dashboard>();

            var baseDashboardsUser = dashboardRepository.GetAll(customer.UserId).ToList();

            if (baseDashboardsUser.Count() > 0)
            {
                baseDashboardsUser.ForEach(x => dashboardsToSave.Add(x));
            }

            foreach (var item in dashboards)
            {
                dashboardsToSave.Add(new Dashboard(new Guid(item.Id) , item.Title,item.Order, item.Url, new Guid(item.CustomerId)));
            }


            dashboardRepository.SaveAll(dashboardsToSave);

        }

        public static void RegisterCustomerRepository(RegisterCustomerInput command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var user = new User(command.Username, command.Password, command.ConfirmPassword);
            var customer = new Customer(name, email, user, document, command.Phone);

            var nameContract = new NameContract(name);
            var documentContract = new DocumentContract(document);
            var emailContract = new EmailContract(email);
            var userContract = new UserContract(user);
            var customerContract = new CustomerContract(customer, nameContract, emailContract, userContract, documentContract);

            if (customerContract.Contract.Valid)
            {
                var context = new MyDashboardDataContext();
                var repository = new CustomerRepository(context);

                var exists = repository.DocumentExist(command.Document);

                if (!exists)
                {
                    customer.Active();
                    repository.Save(customer);
                }
                else
                {
                    customerContract.Contract.AddNotification(new Notification("Document", "Cpf/Cnpj já cadastrado"));
                }
            }
            else
            {
                var msm = customerContract.Contract.Notifications;
            }
        }



        public static string EncryptPassword(string pass)
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
