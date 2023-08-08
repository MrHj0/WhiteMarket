using Microsoft.EntityFrameworkCore;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Groups;
using WhiteMarket.Presistence.EF.ProductEntryFactors;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Presistence.EF.SaleAccountingFactors;
using WhiteMarket.Presistence.EF.SaleCustomerFactors;
using WhiteMarket.Services.Contracts;
using WhiteMarket.Services.Groups;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.ProductEntryFactors;
using WhiteMarket.Services.ProductEntryFactors.Contracts;
using WhiteMarket.Services.Products;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<GroupService, GroupAppService>();
builder.Services.AddScoped<GroupRepository, EFGroupRepository>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<ProductService, ProductAppService>();
builder.Services.AddScoped<ProductRepository, EFProductRepository>();
builder.Services.AddScoped<ProductEntryFactorDateTime, ProductEntryFactorDateTimeImpliment>();
builder.Services.AddScoped<ProductEntryFactorService, ProductEntryFactorAppService>();
builder.Services.AddScoped<SaleCustomerFactorService, SaleCustomerFactorAppService>();
builder.Services.AddScoped<SaleCustomerFactorRepository, EFSaleCustomerFactorRepository>();
builder.Services.AddScoped<SaleCustomerFactorDateTime, SaleCustomerFactorDateTimeImpliment>();
builder.Services.AddScoped<SaleAccountingFactorGuidGenerator, SaleAccountingFactorGuidGeneratorImpliment>();
builder.Services.AddScoped<ProductEntryFactorRepository, EFProductEntryFactorRepository>();
builder.Services.AddScoped<SaleAccountingFactorRepository, EFSaleAccountingFactorRepository>();

builder.Services.AddDbContext<EFDataContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("SqlServer"));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
