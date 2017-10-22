using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Domain.Commands.Handlers;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Infra.Transactions;
using System;
using System.Threading.Tasks;

namespace MyDashboard.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerHandler _handler;

        public CustomerController(IUow uow, CustomerHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/customer/{id}")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> Get(Guid id)
        {
            var command = new GetCustomerInput() { id = id };
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }

        [HttpPost]
        [Route("v1/customer")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerInput command)
        {
            command.Password = command.Username + "123";
            command.ConfirmPassword = command.Username + "123";
            command.Username = command.FirstName + command.LastName;

            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }

        [HttpPut]
        [Route("v1/customer")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Put([FromBody]UpdateCustomerInput command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }

        [HttpDelete]
        [Route("v1/customer")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteCustomerInput() { Id = new Guid(id) };
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }   



    }
}
