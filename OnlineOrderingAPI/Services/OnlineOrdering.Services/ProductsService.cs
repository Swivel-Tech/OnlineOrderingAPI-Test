using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Common.Utils;
using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.Services
{
    public class ProductsService: BaseService, IProductsService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper): base(mapper)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ProductDto>> GetAllProducts()
        {
             var products = await unitOfWork.Products.GetAll().ToListAsync();
             return AutoMapperUtility<IList<Product>, IList<ProductDto>>.GetMappedObject(products, mapper);
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="OnlineOrdering.Common.Exceptions.CustomException">Product not found.</exception>
        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await unitOfWork.Products.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
            if (product != null)
                return AutoMapperUtility<Product?, ProductDto?>.GetMappedObject(product, mapper);
            else
                throw new CustomException("Product not found.", System.Net.HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="request">The request.</param>
        public async Task<ProductDto> CreateProduct(CreateProductRequest request)
        {
            var product = AutoMapperUtility<CreateProductRequest, Product>.GetMappedObject(request, mapper);
            await  unitOfWork.Products.CreateAsync(product);
            await unitOfWork.Complete();
            return AutoMapperUtility<Product, ProductDto>.GetMappedObject(product, mapper);
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <returns></returns>
        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            var product = AutoMapperUtility<ProductDto, Product>.GetMappedObject(productDto, mapper);
            unitOfWork.Products.Update(product);
            await unitOfWork.Complete();
            return AutoMapperUtility<Product, ProductDto>.GetMappedObject(product, mapper);
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteProduct(int id)
        {
            var product = await unitOfWork.Products.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null)
                throw new CustomException("Product not found.", System.Net.HttpStatusCode.BadRequest);

            product.IsActive = false;

            unitOfWork.Products.Delete(product);
            await unitOfWork.Complete();
        }
    }
}
