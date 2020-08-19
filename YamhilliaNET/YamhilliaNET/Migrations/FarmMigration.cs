using FluentMigrator;
using YamhilliaNET.Migrations.Extensions;

namespace YamhilliaNET.Migrations
{
    [Migration(2)]
    public class FarmMigration: Migration
    {
        public override void Up()
        {
            Create.Table("Farms")
                .WithCommonStructure()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("OwnerId").AsInt64().NotNullable();
            Create.ForeignKey().FromTable("Farms").ForeignColumn("OwnerId").ToTable("Users").PrimaryColumn("Id");
        }
    
        public override void Down()
        {
        
        }
    }
}