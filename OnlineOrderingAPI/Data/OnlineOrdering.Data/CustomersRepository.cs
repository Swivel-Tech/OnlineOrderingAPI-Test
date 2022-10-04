using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;

namespace OnlineOrdering.Data
{
    public class CustomersRepository: BaseRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(OnlineOrderingDBContext dBContext) : base(dBContext) { }
    }
}
