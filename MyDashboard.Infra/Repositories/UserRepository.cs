using Dapper;
using MyDashboard.Domain.Commands.Results;
using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Repositories;
using MyDashboard.Infra.Context;
using MyDashboard.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MyDashboard.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDashboardDataContext _context;

        public UserRepository(MyDashboardDataContext context)
        {
            _context = context;
        }

        public User Get(Guid id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GetUserResult> GetByFilter(Guid id, string document, string dashboardName, string userName)
        {
            //var stringConn = @"Server=(LocalDb)\LocalNT; Integrated Security=true; AttachDbFileName=C:\DB\MyDashboard.mdf;User Id=usergemni;Password=pw23";
            var query = "select A.Id, A.FirstName, A.Number from Customer as A inner join UserDashboard as B on (B.Id = A.UserId ) left join Dashboard as C on (C.UserId = B.Id) where ";
            query += $" A.Enable = 1 and ";
            query += $" ( ({userName} is null or A.FirstName like '%'+{userName}+'%')";
            query += $" or ({userName} is null or B.Name like '%'+{userName}+'%')";
            query += $" or ({document} is null or A.Number like '%'+{document}+'%')";
            query += $" or ({dashboardName} is null or C.Title like '%'+{dashboardName}+'%') )";

            using (var conn = new SqlConnection(RunTime.ConnectionString))
            {
                conn.Open();
                return conn.Query<GetUserResult>(query);
            }
        }
    }
}
