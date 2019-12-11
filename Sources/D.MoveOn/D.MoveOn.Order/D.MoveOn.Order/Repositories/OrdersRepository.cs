using D.MoveOn.Common.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Order.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<D.MoveOn.Order.Domain.Order> _repository;

        public OrdersRepository(IMongoRepository<D.MoveOn.Order.Domain.Order> repository)
            => _repository = repository;

        public async Task<D.MoveOn.Order.Domain.Order> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(D.MoveOn.Order.Domain.Order order)
            => await _repository.AddAsync(order);

        public async Task UpdateAsync(D.MoveOn.Order.Domain.Order order)
            => await _repository.UpdateAsync(order);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
