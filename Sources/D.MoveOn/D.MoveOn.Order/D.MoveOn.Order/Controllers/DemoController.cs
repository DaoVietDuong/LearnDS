using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D.MoveOn.Common.Mvc;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Common;
using D.MoveOn.Order.Messages.Commands;
using D.MoveOn.Common.Dispatchers;
using D.MoveOn.Order.Repositories;

namespace D.MoveOn.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IServiceInfor _serviceInfor;
        private readonly IBusPublisher _busPublisher;
        private readonly IDispatcher _dispatcher;
        private readonly IOrdersRepository _ordersRepository;

        public DemoController(IServiceInfor serviceInfor, IBusPublisher busPublisher, IDispatcher dispatcher, IOrdersRepository ordersRepository)
        {
            this._serviceInfor = serviceInfor;
            this._busPublisher = busPublisher;
            this._dispatcher = dispatcher;
            this._ordersRepository = ordersRepository;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateItem(CreateOrderCommand command)
        {
            await _dispatcher.SendAsync(command);
            return Accepted();
        }

        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _ordersRepository.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("GetFake/{id}")]
        public IActionResult GetFake(int id) {

            var resultFake = new List<string>(){
                "data1a and" + id.ToString(),
                "data2  and" + _serviceInfor.Id
            };

            return Ok(resultFake);
        }
    }
}