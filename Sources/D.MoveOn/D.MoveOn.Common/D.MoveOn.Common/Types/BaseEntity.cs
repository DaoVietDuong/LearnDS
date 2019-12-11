using System;
using System.Collections.Generic;
using System.Text;

namespace D.MoveOn.Common.Types
{
    public abstract class BaseEntity : IIdentifiable
    {
        public Guid Id { get; protected set; }

    }
}
