namespace PhoneBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        PhoneNum = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.PhoneNum, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "PhoneNum" });
            DropTable("dbo.Items");
        }
    }
}
