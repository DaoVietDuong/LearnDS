using System;
using System.Collections.Generic;
using System.Text;

namespace D.MoveOn.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
