using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Domain.Commands.Handlers;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Infra.Transactions;
using System;
using System.Threading.Tasks;

namespace MyDashboard.Api.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly DashboardHandler _handler;

        public DashboardController(IUow uow, DashboardHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/dashboard")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post(RegisterDashboardInput command)
        {
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);

            //if (_handler.Valid)
            //{
            //    _uow.Commit();
            //    return Ok(result);
            //}
            //    
            //else
            //    return BadRequest(_handler.Notifications);
        }

        [HttpGet]
        [Route("v1/dashboard/{id}/{userid}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Get(string id, string userid)
        {
            var command = new GetDashboardInput() { Id = new Guid(id),  UserId = new Guid(userid)};
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

        [HttpGet]
        [Route("v1/dashboarduser/{userid}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetByUser(string userid)
        {
            var command = new GetDashboardInput() { Id = Guid.Empty, UserId = new Guid(userid)};
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

        [HttpGet]
        [Route("v1/dashboard/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            var command = new GetDashboardInput() { Id = new Guid(id), UserId = Guid.Empty };
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }


    }
}
