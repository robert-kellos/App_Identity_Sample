namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        IsDone = c.Boolean(nullable: false),
                        TodoListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TodoLists", t => t.TodoListId, cascadeDelete: true)
                .Index(t => t.TodoListId);
            
            CreateTable(
                "dbo.TodoLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoItems", "TodoListId", "dbo.TodoLists");
            DropIndex("dbo.TodoItems", new[] { "TodoListId" });
            DropTable("dbo.TodoLists");
            DropTable("dbo.TodoItems");
        }
    }
}
