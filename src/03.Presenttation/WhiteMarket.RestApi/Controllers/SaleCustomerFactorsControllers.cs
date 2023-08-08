using Microsoft.AspNetCore.Mvc;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto;

namespace WhiteMarket.RestApi.Controllers
{
    [Route("sale_customer_factor")]
    [ApiController]
    public class SaleCustomerFactorsControllers
    {
        private readonly SaleCustomerFactorService _service;

        public SaleCustomerFactorsControllers(SaleCustomerFactorService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Register([FromBody]AddSaleCustomerFactorDto dto)
        {
            _service.Register(dto);
        }
    }
}
