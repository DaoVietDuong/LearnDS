using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Order.Repositories
{
    public interface IOrdersRepository
    {
        Task<D.MoveOn.Order.Domain.Order> GetAsync(Guid id);
        Task AddAsync(D.MoveOn.Order.Domain.Order order);
        Task UpdateAsync(D.MoveOn.Order.Domain.Order order);
        Task DeleteAsync(Guid id);
    }
}
