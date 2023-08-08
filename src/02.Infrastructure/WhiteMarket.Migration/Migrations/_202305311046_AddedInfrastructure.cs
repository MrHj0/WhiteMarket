using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Migration.Migrations
{
    [FluentMigrator.Migration(202305311046)]
    public class _202305311046_AddedInfrastructure : FluentMigrator.Migration
    {
        public override void Up()
        {
            CreateGroups();
            CreateProducts();
            CreateProductEntryFactors();
            CreateSaleAccountingFactors();
            CreateSaleCustomerFactors();
        }

        public override void Down()
        {
            Delete.Table("SaleCustomerFactors");
            Delete.Table("SaleAccountingFactors");
            Delete.Table("ProductEntryFactors");
            Delete.Table("Products");
            Delete.Table("Groups");
        }

        private void CreateSaleAccountingFactors()
        {
            Create.Table("SaleAccountingFactors")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("TotalPrice").AsInt32().NotNullable()
                .WithColumn("CustomerFactorId").AsString(50).NotNullable()
                .WithColumn("Date").AsDateTime2().NotNullable();
        }
        private void CreateSaleCustomerFactors()
        {
            Create.Table("SaleCustomerFactors")
                .WithColumn("DummyPrimaryKey").AsInt32().PrimaryKey()
                .WithColumn("Id").AsString(50).NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("ProductName").AsString(50).NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("CustomerName").AsString(50).NotNullable()
                .WithColumn("Date").AsDateTime2().NotNullable();
        }
        private void CreateProductEntryFactors()
        {
            Create.Table("ProductEntryFactors")
                .WithColumn("DummyPrimaryKey").AsInt32().PrimaryKey()
                .WithColumn("Id").AsString(50).NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("ProductEntryCount").AsInt32().NotNullable()
                .WithColumn("ProductName").AsString(50).NotNullable()
                .WithColumn("CompanyName").AsString(50).NotNullable()
                .WithColumn("Date").AsDateTime2().NotNullable();
        }
        private void CreateGroups()
        {
            Create.Table("Groups")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable();
        }
        private void CreateProducts()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Inventory").AsInt32().NotNullable()
                .WithColumn("MinimumInventory").AsInt32().NotNullable()
                .WithColumn("GroupId").AsInt32().NotNullable()
                .ForeignKey("FK_Products_Groups"
                           ,"Groups", "Id");
        }
    }


}
