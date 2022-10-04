using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;

namespace OnlineOrdering.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly OnlineOrderingDBContext dbContext;

        public ICustomersRepository Customers { get; private set; }
        public IProductsRepository Products { get; private set; }
        public IOrderHeaderRepository Orders { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dBContext">The d b context.</param>
        public UnitOfWork(OnlineOrderingDBContext dBContext)
        {
            dbContext = dBContext;
            Customers = new CustomersRepository(dBContext);
            Products = new ProductsRepository(dBContext);
            Orders = new OrderHeaderRepository(dBContext);
        }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
