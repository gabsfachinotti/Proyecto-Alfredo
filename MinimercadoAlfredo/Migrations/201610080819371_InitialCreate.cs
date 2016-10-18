namespace MinimercadoAlfredo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCategory = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        CategoryDescription = c.String(),
                    })
                .PrimaryKey(t => t.IdCategory);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IdProduct = c.Int(nullable: false, identity: true),
                        ProductDescription = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WholeSalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublicPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UploadDate = c.DateTime(nullable: false),
                        Stock = c.Single(nullable: false),
                        Minimum = c.Single(nullable: false),
                        State = c.Boolean(nullable: false),
                        Image = c.String(),
                        idCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduct)
                .ForeignKey("dbo.Categories", t => t.idCategory, cascadeDelete: true)
                .Index(t => t.idCategory);
            
            CreateTable(
                "dbo.ProductProviders",
                c => new
                    {
                        IdProductProvider = c.Int(nullable: false, identity: true),
                        IdProvider = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProductProvider)
                .ForeignKey("dbo.Products", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.IdProvider, cascadeDelete: true)
                .Index(t => t.IdProvider)
                .Index(t => t.IdProduct);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        IdProvider = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(nullable: false),
                        ProviderAddress = c.String(),
                        ProviderPhone = c.Int(nullable: false),
                        ProviderEmail = c.String(),
                    })
                .PrimaryKey(t => t.IdProvider);
            
            CreateTable(
                "dbo.PurchaseLines",
                c => new
                    {
                        IdPurchaseLine = c.Int(nullable: false, identity: true),
                        PurchaseQuantity = c.Single(nullable: false),
                        Cost = c.Single(nullable: false),
                        LineTotal = c.Single(nullable: false),
                        IdPurchase = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPurchaseLine)
                .ForeignKey("dbo.Products", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.IdPurchase, cascadeDelete: true)
                .Index(t => t.IdPurchase)
                .Index(t => t.IdProduct);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        IdPurchase = c.Int(nullable: false, identity: true),
                        PurchaseDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        PurchaseTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdProvider = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPurchase)
                .ForeignKey("dbo.Providers", t => t.IdProvider, cascadeDelete: true)
                .Index(t => t.IdProvider);
            
            CreateTable(
                "dbo.SaleLines",
                c => new
                    {
                        IdSaleLine = c.Int(nullable: false, identity: true),
                        Discount = c.Int(nullable: false),
                        SaleQuantity = c.Single(nullable: false),
                        LineTotal = c.Single(nullable: false),
                        IdSale = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSaleLine)
                .ForeignKey("dbo.Products", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.IdSale, cascadeDelete: true)
                .Index(t => t.IdSale)
                .Index(t => t.IdProduct);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        IdSale = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        Discount = c.Int(nullable: false),
                        Comments = c.String(),
                        SaleTotal = c.Single(nullable: false),
                        IdCustomer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSale)
                .ForeignKey("dbo.Customers", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        IdCustomer = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        CustomerAddress = c.String(),
                        CustomerPhone = c.Int(nullable: false),
                        CustomerEmail = c.String(),
                        CuitCuil = c.String(),
                    })
                .PrimaryKey(t => t.IdCustomer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleLines", "IdSale", "dbo.Sales");
            DropForeignKey("dbo.Sales", "IdCustomer", "dbo.Customers");
            DropForeignKey("dbo.SaleLines", "IdProduct", "dbo.Products");
            DropForeignKey("dbo.PurchaseLines", "IdPurchase", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "IdProvider", "dbo.Providers");
            DropForeignKey("dbo.PurchaseLines", "IdProduct", "dbo.Products");
            DropForeignKey("dbo.ProductProviders", "IdProvider", "dbo.Providers");
            DropForeignKey("dbo.ProductProviders", "IdProduct", "dbo.Products");
            DropForeignKey("dbo.Products", "idCategory", "dbo.Categories");
            DropIndex("dbo.Sales", new[] { "IdCustomer" });
            DropIndex("dbo.SaleLines", new[] { "IdProduct" });
            DropIndex("dbo.SaleLines", new[] { "IdSale" });
            DropIndex("dbo.Purchases", new[] { "IdProvider" });
            DropIndex("dbo.PurchaseLines", new[] { "IdProduct" });
            DropIndex("dbo.PurchaseLines", new[] { "IdPurchase" });
            DropIndex("dbo.ProductProviders", new[] { "IdProduct" });
            DropIndex("dbo.ProductProviders", new[] { "IdProvider" });
            DropIndex("dbo.Products", new[] { "idCategory" });
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleLines");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseLines");
            DropTable("dbo.Providers");
            DropTable("dbo.ProductProviders");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
