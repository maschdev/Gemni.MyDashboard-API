using MyDashboard.Shared.Commands;
using System;
using System.Collections.Generic;

namespace MyDashboard.Domain.Commands.Inputs
{
    public class RegisterDashboardInput : ICommand
    {
        public RegisterDashboardInput()
        {
            Dashboards = new List<Dashboard>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public string CustomerId { get; set; }

        public IList<Dashboard> Dashboards { get; private set; }


        public class Dashboard
        {
            public Dashboard(string id, string title, int order, string url, string customerId)
            {
                Id = id == "0" ? Guid.Empty : new Guid(id);
                Title = title;
                Order = order;
                Url = url;
                CustomerId = new Guid(customerId);
            }

            public Guid Id { get; private set; }
            public string Title { get; private set; }
            public int Order { get; private set; }
            public string Url { get; private set; }
            public Guid CustomerId { get; private set; }
        }

        
        public void AddDashboards(string id, string title, int order, string url, string customerId) =>
            Dashboards.Add(new Dashboard(id, title, order, url, customerId));

    }
}
