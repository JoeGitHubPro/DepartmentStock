namespace DepartmentStock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Degree", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Degree");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
