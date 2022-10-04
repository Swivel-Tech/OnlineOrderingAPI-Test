using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ProductDto>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await productsService.GetAllProducts());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await productsService.GetProductById(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderHeaderDto), 200)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            return Ok(await productsService.CreateProduct(request));
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderHeaderDto), 200)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            return Ok(await productsService.UpdateProduct(productDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery]int productId)
        {
            await productsService.DeleteProduct(productId);
            return Ok();
        }
    }
}
