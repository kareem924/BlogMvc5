namespace BlogMvc5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.post_Id)
                .Index(t => t.post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Posts_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Posts_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Posts_Id, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Posts_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Posts_Id", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tag");
            DropForeignKey("dbo.Comment", "post_Id", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Posts_Id" });
            DropIndex("dbo.TagPosts", new[] { "Tag_TagId" });
            DropIndex("dbo.Comment", new[] { "post_Id" });
            DropTable("dbo.TagPosts");
            DropTable("dbo.Page");
            DropTable("dbo.Tag");
            DropTable("dbo.Posts");
            DropTable("dbo.Comment");
        }
    }
}
