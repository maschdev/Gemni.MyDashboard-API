﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Domain.Commands.Handlers;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Infra.Transactions;
using System;
using System.Threading.Tasks;

namespace MyDashboard.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserHandler _handler;

        public UserController(IUow uow, UserHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/user/{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Get(string id)
        {
            var command = new GetUserInput() { Id = new Guid(id) };
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

        [HttpGet]
        [Route("v1/user/{document}/{dashboardname}/{username}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetByFilter( string document, string dashboardname, string username)
        {
            var command = new GetUserInput()
            {
                Document = document == "null" ? null : document,
                DashboardName = dashboardname == "null" ? null : dashboardname,
                UserName = username == "null" ? null : username
            };

            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

        [HttpGet]
        [Route("v1/userdbinfo/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUserDashboardInfo(string id)
        {
            var command = new GetUserDashboardInfoInput() { Id = new Guid(id) };
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

    }
}
