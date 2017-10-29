using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Repositories;
using MyDashboard.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyDashboard.Infra.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MyDashboardDataContext _context;

        public DashboardRepository(MyDashboardDataContext context)
        {
            _context = context;
        }

        public Dashboard GetByUser(Guid id, Guid userId)
        {
            return _context.Dashboards.AsNoTracking().FirstOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public Dashboard Get(Guid id)
        {
            return _context.Dashboards.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Dashboard> GetAll(Guid userId)
        {
            return _context.Dashboards.AsNoTracking().Where(x => x.UserId == userId).OrderBy(x => x.Order);
        }

        public void Save(Dashboard dashboard)
        {
            _context.Dashboards.Add(dashboard);
            //_context.SaveChanges();// Sera alterado
        }

        public void SaveAll(IList<Dashboard> dashboards)
        {
            DeleteDashboardsRemoved(dashboards);

            foreach (var dashboard in dashboards)
            {
                var dashboardRepository = GetByUser(dashboard.Id, new Guid(dashboard.UserId.ToString()));

                if (dashboardRepository == null)
                {
                    Save(dashboard);
                }
                else
                {
                    dashboardRepository.Update(dashboard.Title, dashboard.Order, dashboard.Url);
                    Update(dashboardRepository);
                }
            };
           
        }

        public void Update(Dashboard dashboard)
        {
            _context.Entry(dashboard).State = EntityState.Modified;
            //_context.SaveChanges(); // Sera alterado
        }

        public void DeleteDashboardsRemoved(IList<Dashboard> dashboards)
        {
            var dashboardsIdsByUser = GetAll(new Guid(dashboards[0].UserId.ToString())).Select(x => x.Id);

            if (dashboardsIdsByUser.Count() <= 0)
                return;

            var dashboardsIdsInput = dashboards.Select(x => x.Id);
            var dashboardsToDelete = dashboardsIdsByUser.Except(dashboardsIdsInput);

            if (dashboardsToDelete.Count() <= 0)
                return;

            foreach (var id in dashboardsToDelete)
            {
                Delete(id);
            }
        }

        public void Delete(Guid id)
        {
            var dashboard = Get(id);
            _context.Dashboards.Remove(dashboard);
            //_context.SaveChanges(); // Sera alterado
        }
    }
}
