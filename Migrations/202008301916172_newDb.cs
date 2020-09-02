namespace MyCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ConfirmEmail = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        FoodName = c.String(nullable: false),
                        FoodPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TotalBill = c.Double(nullable: false),
                        Status = c.String(),
                        MenuId = c.Int(),
                        CustId = c.Int(),
                        VendId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdId)
                .ForeignKey("dbo.Customers", t => t.CustId)
                .ForeignKey("dbo.Menus", t => t.MenuId)
                .ForeignKey("dbo.Vendors", t => t.VendId)
                .Index(t => t.MenuId)
                .Index(t => t.CustId)
                .Index(t => t.VendId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ConfirmEmail = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "VendId", "dbo.Vendors");
            DropForeignKey("dbo.Orders", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Orders", "CustId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "VendId" });
            DropIndex("dbo.Orders", new[] { "CustId" });
            DropIndex("dbo.Orders", new[] { "MenuId" });
            DropTable("dbo.Vendors");
            DropTable("dbo.Orders");
            DropTable("dbo.Menus");
            DropTable("dbo.Customers");
        }
    }
}
