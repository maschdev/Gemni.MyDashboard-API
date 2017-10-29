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

        public DbUserDashboardInfoResult GetUserDashboardInfo(Guid id)
        {
            var query = "select B.FirstName as [Name], B.Number as [Document], B.Email, count(C.Id) as [Quantity] from UserDashboard as A ";
            query += "inner join Customer as B on (B.UserId = A.Id) ";
            query += "left join Dashboard as C on (C.UserId = A.Id) ";
            query += "where B.Enable = 1 and B.Id = '" + id.ToString() + "' ";
            query += "group by B.FirstName, B.Email, B.Number;";

            using (var conn = new SqlConnection(RunTime.ConnectionString))
            {
                conn.Open();
                return conn.Query<DbUserDashboardInfoResult>(query).FirstOrDefault();
            }
        }

        public IEnumerable<DbUserByFilterResult> GetByFilter(string document, string dashboardName, string userName)
        {
            var query = "";

            var _document = "declare @number nchar(28) = ";
            _document += string.IsNullOrWhiteSpace(document) ? "null; " : "'"+ document + "'; ";
            query += _document;

            var _dashboardName = " declare @title nvarchar(40) = ";
            _dashboardName += string.IsNullOrWhiteSpace(dashboardName) ? "null; " : "'" + dashboardName + "'; ";
            query += _dashboardName;

            var _userName = " declare @customer nvarchar(120) = ";
            _userName += string.IsNullOrWhiteSpace(userName) ? "null; " : "'" + userName + "'; ";
            query += _userName;

            query += " select A.Id as [Id], A.FirstName as [Name], A.Number as [Document] from Customer as A inner join UserDashboard as B on (B.Id = A.UserId ) left join Dashboard as C on (C.UserId = B.Id) where ";
            query += " A.Enable = 1 and";
            query += " ( ((@customer is null or A.FirstName like '%'+ @customer + '%')";
            query += " or (@customer is null or B.Name like '%' + @customer + '%'))";
            query += " and (@number is null or A.Number like '%' + @number + '%')";
            query += " and (@title is null or C.Title like '%' + @title + '%') )";
            query += " group by A.Id, A.FirstName, A.Number ";

            using (var conn = new SqlConnection(RunTime.ConnectionString))
            {
                conn.Open();
                return conn.Query<DbUserByFilterResult>(query);
            }
        }
    }
}
