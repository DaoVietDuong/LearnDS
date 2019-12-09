using System;
using System.Threading.Tasks;
using D.MoveOn.Common;
using D.MoveOn.Common.Handlers;
using JD.MoveOn.Product.Messages.Commands;

namespace JD.MoveOn.Product.Handlers
{
    public class CreateItemHandler : ICommandHandler<CreateItemCommand>
    {
        public Task HandleAsync(CreateItemCommand command, ICorrelationContext context)
        {
            Console.WriteLine($"Hello {command.Name}");

            return Task.CompletedTask;
        }
    }
}
