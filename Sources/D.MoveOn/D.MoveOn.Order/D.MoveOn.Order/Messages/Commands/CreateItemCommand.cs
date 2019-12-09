using D.MoveOn.Common.Messages;
using Newtonsoft.Json;

namespace D.MoveOn.Demo.Messages.Commands
{
    [MessageNamespace("demo2")]
    public class CreateItemCommand : ICommand
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public CreateItemCommand(int id,
                         string name)
        {
            Id = id;
            Name = name;
        }
    }
}
