using Microsoft.AspNetCore.Mvc;
using WhiteMarket.Services.ProductEntryFactors.Contracts;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;

namespace WhiteMarket.RestApi.Controllers
{
    [Route("product-entry-factors")]
    [ApiController]
    public class ProductEntryFactorsController : Controller
    {
        private readonly ProductEntryFactorService _service;

        public ProductEntryFactorsController(ProductEntryFactorService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Register([FromBody]AddProductEntryFactorDto dto)
        {
            _service.Register(dto);
        }

    }
}
