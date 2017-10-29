using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Repositories;
using MyDashboard.Infra.Context;
using System;
using System.Data.Entity;
using System.Linq;

namespace MyDashboard.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDashboardDataContext _context;

        public CustomerRepository(MyDashboardDataContext context)
        {
            _context = context;
        }

        public Customer GetByUserEmail(string username)
        {
            return _context.Customers
                .Include(x => x.User)
                .AsNoTracking().FirstOrDefault(x => x.Email.Address.Trim() == username.Trim());
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DocumentExist(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public Customer Get(Guid id)
        {
            return _context.Customers.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
