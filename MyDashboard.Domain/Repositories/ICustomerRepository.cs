using MyDashboard.Domain.Entities;
using System;

namespace MyDashboard.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(Guid id);

        //GetCustomerCommandResult Get(string username);

        Customer GetByUserEmail(string username);

        bool DocumentExist(string document);

        void Save(Customer customer);

        void Update(Customer customer);

        void Delete(Guid id);

    }
}
