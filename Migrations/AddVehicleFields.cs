using System;
using System.Data.Entity.Migrations;

namespace Diplom.Migrations
{
    public partial class AddVehicleFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "RegistrationRegion", c => c.String(maxLength: 100, nullable: true));
            AddColumn("dbo.Vehicles", "EnginePower", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "EnginePower");
            DropColumn("dbo.Vehicles", "RegistrationRegion");
        }
    }
} 