using MyDashboard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Repositories
{
    public interface IDashboardRepository
    {
        IEnumerable<Dashboard> GetAll(Guid userId);

        Dashboard GetByUser(Guid id, Guid userId);

        Dashboard Get(Guid id);

        void Save(Dashboard dashboard);

        void Update(Dashboard dashboard);

        void Delete(Guid id);

        void SaveAll(IList<Dashboard> dashboard);
    }
}
