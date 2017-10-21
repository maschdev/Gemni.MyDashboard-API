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

namespace MyDashboard.Domain.Commands.Handlers
{
    public class DashboardHandler : Notifiable,
        ICommandHandler<RegisterDashboardInput>,
        ICommandHandler<UpdateDashboardInput>,
        ICommandHandler<DeleteDashboardInput>,
        ICommandHandler<GetDashboardInput>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public ICommandResult Handle(RegisterDashboardInput command)
        {
            var dashboards = new List<Dashboard>();
            var dashboardContracts = new List<DashboardContract>();

            foreach (var item in command.Dashboards)
            {
                dashboards.Add(new Dashboard(item.Id, item.Title, item.Order, item.Url, item.UserId));
                dashboardContracts.Add(new DashboardContract(new Dashboard(item.Id, item.Title, item.Order, item.Url, item.UserId)));
            }

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
                var dashboard = _dashboardRepository.GetByUser(command.Id, command.UserId);

                result.Title = dashboard.Title;
                result.Url = dashboard.Url;
                result.Order = dashboard.Order.ToString();
                result.UserId = dashboard.UserId.ToString();
            }

            // GetAll Dashboards by userid
            if (command.Id == Guid.Empty && command.UserId != Guid.Empty)
            {
                var dashboards = _dashboardRepository.GetAll(command.UserId);

                foreach (var item in dashboards)
                {
                    result.Dashboards.Add(
                            new GetDashboardResult.Dashboard()
                            {
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

                result.Title = dashboard.Title;
                result.Url = dashboard.Url;
                result.Order = dashboard.Order.ToString();
                result.UserId = dashboard.UserId.ToString();
            }

            return result;
        }
    }
}
