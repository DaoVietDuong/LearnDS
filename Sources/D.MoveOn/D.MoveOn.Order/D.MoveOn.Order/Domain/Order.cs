using D.MoveOn.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Order.Domain
{
    public class Order : BaseEntity
    {
        public string Name { get; private set; }
        public Guid ProductId { get; private set; }

        public Order(Guid id, string name, Guid productId)
        {
            Id = id;
            Name = name;
            ProductId = productId;
        }
    }
}
