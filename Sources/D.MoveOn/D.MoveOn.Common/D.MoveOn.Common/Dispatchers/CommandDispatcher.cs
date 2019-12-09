using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using D.MoveOn.Common.Handlers;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            this._context = context;
        }
        public async Task SendAsync<T>(T command) where T : ICommand 
        => await _context.Resolve<ICommandHandler<T>>().HandleAsync(command, CorrelationContext.Empty);

    }
}
