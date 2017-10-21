using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<GetUserResult> GetByFilter(Guid id, string document, string dashboardName, string userName);

        User Get(Guid id);
    }
}
