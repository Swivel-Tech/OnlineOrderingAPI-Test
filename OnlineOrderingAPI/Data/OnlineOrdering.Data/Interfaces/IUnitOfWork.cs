namespace OnlineOrdering.Data.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ICustomersRepository Customers { get; }
        IProductsRepository Products { get; }
        IOrderHeaderRepository Orders { get; }
        Task<int> Complete();
    }
}
