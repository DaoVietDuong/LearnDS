using System;
using System.Collections.Generic;
using System.Text;

namespace D.MoveOn.Common
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}
