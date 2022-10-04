using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;

namespace OnlineOrdering.Data
{
    public class OrderHeaderRepository: BaseRepository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepository(OnlineOrderingDBContext dBContext) : base(dBContext) { }
    }
}
