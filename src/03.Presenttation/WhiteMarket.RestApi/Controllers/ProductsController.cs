using Microsoft.AspNetCore.Mvc;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.RestApi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Define([FromBody]AddProductDto dto)
        {
            _service.Define(dto);
        }

        [HttpGet]
        public HashSet<GetAllProductsDto> GetAll([FromQuery]ProductSearchDto? search)
        {
            return _service.GetAllProducts(search);
        }
    }
}

