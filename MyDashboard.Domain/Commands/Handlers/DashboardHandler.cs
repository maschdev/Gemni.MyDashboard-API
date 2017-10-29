using FluentValidator;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Repositories;
using MyDashboard.Domain.Entities;
using MyDashboard.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using MyDashboard.Domain.Contracts;
using System.Threading.Tasks;

namespace MyDashboard.Domain.Commands.Handlers
{
    public class DashboardHandler : Notifiable,
        ICommandHandler<RegisterDashboardInput>,
        ICommandHandler<UpdateDashboardInput>,
        ICommandHandler<DeleteDashboardInput>,
        ICommandHandler<GetDashboardInput>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ICustomerRepository _customerRepository;

        public DashboardHandler(IDashboardRepository dashboardRepository, ICustomerRepository customerRepository)
        {
            _dashboardRepository = dashboardRepository;
            _customerRepository = customerRepository;
        }

        public ICommandResult Handle(RegisterDashboardInput command)
        {
            var dashboards = new List<Dashboard>();
            var dashboardContracts = new List<DashboardContract>();

            var userId = _customerRepository.Get(command.Dashboards[0].CustomerId).UserId;

            Parallel.ForEach(command.Dashboards, dashboard => 
            {
                dashboards.Add(new Dashboard(dashboard.Id, dashboard.Title, dashboard.Order, dashboard.Url, userId));
                dashboardContracts.Add(new DashboardContract(new Dashboard(dashboard.Id, dashboard.Title, dashboard.Order, dashboard.Url, userId)));
            });

            if (dashboardContracts.Any(x => x.Contract.Invalid))
                return null;

            _dashboardRepository.SaveAll(dashboards);

            return new RegisterDashboardResult() {Valid  =true, Message = "Cadastro com sucesso" };
        }

        public ICommandResult Handle(UpdateDashboardInput command)
        {
            return new UpdateDashboardResult();
        }

        public ICommandResult Handle(DeleteDashboardInput command)
        {
            return new DeleteDashboardResult();
        }

        public ICommandResult Handle(GetDashboardInput command)
        {
            var result = new GetDashboardResult();

            // GetByUser
            if (command.Id != Guid.Empty && command.UserId != Guid.Empty)
            {
                var userid = _customerRepository.Get(command.UserId).UserId;
                var dashboard = _dashboardRepository.GetByUser(command.Id, userid);

                result.Id = result.Id.ToString();
                result.Title = dashboard.Title;
                result.Url = dashboard.Url;
                result.Order = dashboard.Order.ToString();
                result.UserId = dashboard.UserId.ToString();
            }

            // GetAll Dashboards by userid
            if (command.Id == Guid.Empty && command.UserId != Guid.Empty)
            {
                var userid = _customerRepository.Get(command.UserId).UserId;
                var dashboards = _dashboardRepository.GetAll(userid);

                foreach (var item in dashboards)
                {
                    result.Dashboards.Add(
                            new GetDashboardResult.Dashboard()
                            {
                                Id = item.Id.ToString(),
                                Title = item.Title,
                                Url = item.Url,
                                Order = item.Order.ToString(),
                                UserId = item.UserId.ToString()
                            });
                }
            }

            // Get Dashboard by Id dashboard
            if (command.Id != Guid.Empty && command.UserId == Guid.Empty)
            {
                var dashboard = _dashboardRepository.Get(command.Id);

                result.Id = dashboard.Id.ToString();
                result.Title = dashboard.Title;
                result.Url = dashboard.Url;
                result.Order = dashboard.Order.ToString();
                result.UserId = dashboard.UserId.ToString();

            }

            return result;


        }
    }
}
