using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Notification.Infra
{
    public class RedisOptions
    {
        public string ConnectionString { get; set; }
        public string Instance { get; set; }
    }
}
