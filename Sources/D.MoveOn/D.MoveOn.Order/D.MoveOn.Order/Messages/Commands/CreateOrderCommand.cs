using D.MoveOn.Common.Messages;
using Newtonsoft.Json;
using System;

namespace D.MoveOn.Order.Messages.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid ProductId { get; }

        [JsonConstructor]
        public CreateOrderCommand(Guid id,
                         string name, Guid productId)
        {
            Id = id;
            Name = name;
            ProductId = productId;
        }
    }
}
