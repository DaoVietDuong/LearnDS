using D.MoveOn.Common.Messages;
using Newtonsoft.Json;
using System;

namespace D.MoveOn.Notification.Messages.Events
{
    public class ResultProcess : IEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public string Resource { get; }

        [JsonConstructor]
        public ResultProcess(Guid id,
            Guid userId, string name, string resource)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Resource = resource;
        }
    }
}
