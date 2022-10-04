using OnlineOrdering.Data.DatabaseContext;

namespace OnlineOrdering.Data.Interfaces
{
    public class ProductsRepository: BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(OnlineOrderingDBContext dBContext) : base(dBContext) { }
    }
}
