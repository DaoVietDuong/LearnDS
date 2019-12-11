using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace D.MoveOn.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
