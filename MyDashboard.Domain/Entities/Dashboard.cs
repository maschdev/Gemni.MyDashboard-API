using MyDashboard.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDashboard.Domain.Entities
{
    public class Dashboard 
    {
        protected Dashboard() { }

        public Dashboard(Guid id, string title, int order, string url, Guid userId)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Order = order;
            Url = url;
            UserId = userId;
            Active = true;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public int Order { get; private set; }
        public string Url { get; private set; }
        public bool Active { get; private set; }
        public Guid? UserId { get; private set; }
        public virtual User User { get; private set; }

        public void Insert(string title, int order, string url)
        {
            Title = title;
            Order = order;
            Url = url;

            //TODO
        }

        public void Update(string title, int order, string url)
        {
            Title = title;
            Order = order;
            Url = url;

            //TODO
        }


    }
}
