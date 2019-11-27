namespace PoW.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskWorks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    Status = c.Int(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    EditDate = c.DateTime(),
                    RemoveDate = c.DateTime(),
                    CloseDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.TaskWorks");
        }
    }
}
