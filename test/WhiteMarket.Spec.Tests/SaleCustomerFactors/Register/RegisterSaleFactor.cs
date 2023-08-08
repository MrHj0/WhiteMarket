using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Presistence.EF.SaleAccountingFactors;
using WhiteMarket.Presistence.EF.SaleCustomerFactors;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;
using WhiteMarket.TestTools.SaleCustomerFactors;

namespace WhiteMarket.Spec.Tests.SaleCustomerFactors.Register
{
    [Scenario("فروش کالا ")]
    public class RegisterSaleFactor : BusinessIntegrationTest
    {
        private Product _product;
        private Group _group;
        private DateTime _date;
        private Guid _guid;
        private readonly SaleCustomerFactorService _sut;

        public RegisterSaleFactor()
        {
            var dateTime = new Mock<SaleCustomerFactorDateTime>();
            dateTime.Setup(_ => _.Generate()).Returns(DateTime.Now);
            _date = dateTime.Object.Generate();
            var guid = new Mock<SaleAccountingFactorGuidGenerator>();
            guid.Setup(_=>_.Generate()).Returns(Guid.NewGuid());
            _guid = guid.Object.Generate();
            _sut = SaleCustomerFactorAppServiceFactory.Generate(SetupContext, dateTime.Object, guid.Object);
        }

        [Given("گروهی با نام لوازم یدکی در فهرست گروه ها وجود دارد " +
            "و:کالایی با عنوان لنت ترمز با موجودی ۲۰  " +
            "و وضعیت موجود و حداقل موجودی ۵ " +
            "در گروه لوازم یدکی وجود دارد ")]
        public void Given()
        {
            _group = GroupFactory.Generate("اسباب بازی");
            _product = new ProductBuilder(_group)
                .WithTitle("لنت ترمز")
                .WithInventory(20)
                .WithStatus(InventoryStatus.Avalable)
                .WithMinimumInventry(5)
                .Build();
            DbContext.Save(_product);
        }

        [When("فاکتور فروشی با شماره فاکتور ۱۲۳آ و کالایی با عنوان لنت ترمز" +
            "و قیمت واحد ۱۰۰۰ تومان برای مشتری به نام مجید رضوی " +
            "به تعداد ۵ عدد را ثبت میکنم")]

        public void When()
        {
            var dto = new AddSaleCustomerFactorDto
            {
                ProductId = _product.Id,
                Count = 5,
                CustomerName = "مجید رضوی",
                Price = 1000,
                ProductName = _product.Title,
                FactorId = "۱۲۳آ"
            };

            _sut.Register(dto);
        }

        [Then("کالا با عنوان لنت ترمز با موجودی ۱۵ " +
            "و وضعیت موجود و حداقل موجودی ۵ " +
            "در گروه لوازم یدکی وجود داشته باشد" +
            "و: یک فاکتور فروش با کالای لنت ترمز " +
            "و تعداد ۵ و قیمت ۱۰۰۰ و شماره فاکتور ۱۲۳آ " +
            "و مشتری با نام مجید رضوی و تاریخ 1402 " +
            "در فاکتورهای فروش باید وجود داشته باشد" +
            "و: یک سند حسابداری با شماره فاکتور ۱۲۳آ " +
            "و شماره سند 1233455657 و تاریخ 1402 " +
            "و مبلغ ۵۰۰۰ باید در فهرست سندهای حسابداری ثبت شده باشد ‌")]
        public void Then()
        {
            var expectedProduct = ReadContext.Set<Product>().Single();
            expectedProduct.Title.Should().Be("لنت ترمز");
            expectedProduct.Inventory.Should().Be(15);
            expectedProduct.Status.Should().Be(InventoryStatus.Avalable);
            expectedProduct.MinimumInventory.Should().Be(5);
            expectedProduct.GroupId.Should().Be(_group.Id);

            var expectedCustomerFactor = ReadContext.Set<SaleCustomerFactor>().Single();
            expectedCustomerFactor.ProductName.Should().Be("لنت ترمز");
            expectedCustomerFactor.Count.Should().Be(5);
            expectedCustomerFactor.Price.Should().Be(1000);
            expectedCustomerFactor.Id.Should().Be("۱۲۳آ");
            expectedCustomerFactor.CustomerName.Should().Be("مجید رضوی");
            expectedCustomerFactor.Date.Should().Be(_date);

            var expectedAccountingFactor = ReadContext.Set<SaleAccountingFactor>().Single();
            expectedAccountingFactor.CustomerFactorId.Should().Be("۱۲۳آ");
            expectedAccountingFactor.Id.Should().Be(_guid);
            expectedAccountingFactor.Date.Should().Be(expectedCustomerFactor.Date);
            expectedAccountingFactor.TotalPrice.Should().Be(5000);
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
