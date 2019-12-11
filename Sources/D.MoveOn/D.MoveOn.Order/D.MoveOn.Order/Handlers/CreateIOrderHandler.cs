using D.MoveOn.Common;
using D.MoveOn.Common.Handlers;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Order.Messages.Commands;
using D.MoveOn.Order.Repositories;
using D.MoveOn.Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D.MoveOn.Order.Messages.Events;

namespace D.MoveOn.Order.Handlers
{
    public class CreateIOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public CreateIOrderHandler(IBusPublisher busPublisher,
            IOrdersRepository ordersRepository)
        {
            this._busPublisher = busPublisher;
            this._ordersRepository = ordersRepository;
        }
        public async Task HandleAsync(CreateOrderCommand command, ICorrelationContext context)
        {
            var order = new D.MoveOn.Order.Domain.Order(command.Id, command.Name, command.ProductId);
            await _ordersRepository.AddAsync(order);
            await _busPublisher.PublishAsync<ResultProcess>(new ResultProcess(Guid.NewGuid(), context.UserId, "Duong", "test"), context);
        }
    }
}
