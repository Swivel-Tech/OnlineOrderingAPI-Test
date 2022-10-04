using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Services.Interfaces
{
    public interface IProductsService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        Task<IList<ProductDto>> GetAllProducts();

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ProductDto?> GetProductById(int id);

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <returns></returns>
        Task<ProductDto> CreateProduct(CreateProductRequest productDto);

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <returns></returns>
        Task<ProductDto> UpdateProduct(ProductDto productDto);

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteProduct(int id);
    }
}
