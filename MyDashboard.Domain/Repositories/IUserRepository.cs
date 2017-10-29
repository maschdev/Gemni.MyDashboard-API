using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<DbUserByFilterResult> GetByFilter(string document, string dashboardName, string userName);

        DbUserDashboardInfoResult GetUserDashboardInfo(Guid id);

        User Get(Guid id);
    }
}
