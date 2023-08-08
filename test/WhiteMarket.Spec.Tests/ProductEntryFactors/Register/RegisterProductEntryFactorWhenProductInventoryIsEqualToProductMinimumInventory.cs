using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.ProductEntryFactors;
using WhiteMarket.Services.ProductEntryFactors.Contracts;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.ProductEntryFactors;
using WhiteMarket.TestTools.Products;

namespace WhiteMarket.Spec.Tests.ProductEntryFactors.Register
{
    [Scenario("ورود کالا زمانی که موجودی کالا با حداقل موجودی کالا برابر میشود")]
    public class RegisterProductEntryFactorWhenProductInventoryIsEqualToProductMinimumInventory : BusinessIntegrationTest
    {
        private readonly ProductEntryFactorService _sut;
        private DateTime date;
        private Product _product;
        private Group _group;

        public RegisterProductEntryFactorWhenProductInventoryIsEqualToProductMinimumInventory()
        {
            var dateTime = new Mock<ProductEntryFactorDateTime>();
            dateTime.Setup(_ => _.Generate()).Returns(DateTime.Now);
            date = dateTime.Object.Generate();
            _sut = ProductEntryFactorAppServiceFactory.Generate(SetupContext, dateTime.Object);
        }

        [Given("یک گروه با نام بهداشتی در فهرست گروه ها وجود دارد" +
               "و: یک کالا با عنوان شامپو با موجودی ۰ و وضعیت ناموجود  " +
            "و حداقل موجودی ۱۰ در گروه بهداشتی وجود دارد")]
        public void Given()
        {
            _group = GroupFactory.Generate("بهداشتی");

            _product = new ProductBuilder(_group)
                .WithTitle("شامپو")
                .WithMinimumInventry(10)
                .WithStatus(InventoryStatus.UnAvalable)
                .Build();

            DbContext.Save(_product);
        }

        [When("تعداد ۱۰  تا از کالایی با عنوان شامپو " +
            "با شماره فاکتور ۱۲۳آ در نام شرکت فپکو  " +
            "در گروه بهداشتی را وارد میکنم ")]

        public void When()
        {
            var dto = new AddProductEntryFactorDto
            {
                ProductId = _product.Id,
                CompanyName = "فپکو",
                Count = 10,
                FactorId = "۱۲۳آ"
            };

            _sut.Register(dto);
        }

        [Then("یک کالا با عنوان شامپو و موجودی ۱۰ " +
            "و وضعیت اماده ی سفارش  در گروه بهداشتی " +
            "و حداقل موجودی ۱۰ باید در فهرست کالاها وجود داشته باشدد" +
            "و:یک ورودی کالا برای کالای شامپو در تاریخ 1402/09/19 21:59  " +
            "و تعداد ۲۰ و شماره فاکتور ۱۲۳آ " +
            "و نام شرکت فپکو باید " +
            "در فهرست ورودی های کالا وجود داشته باشد")]
        public void Then()
        {
            var expectedProduct = ReadContext.Set<Product>().Single();
            expectedProduct.Title.Should().Be("شامپو");
            expectedProduct.Inventory.Should().Be(10);
            expectedProduct.Status.Should().Be(InventoryStatus.LowInventory);
            expectedProduct.GroupId.Should().Be(_group.Id);
            var expectedProductEntryFactor = ReadContext
            .Set<ProductEntryFactor>().Single();
            expectedProductEntryFactor.Date.Should().Be(date);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then());
        }
    }
}
