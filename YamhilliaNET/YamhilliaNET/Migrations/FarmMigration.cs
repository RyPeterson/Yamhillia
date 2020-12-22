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
                .WithColumn("Name").AsString().NotNullable();
        }
    
        public override void Down()
        {
            Delete.Table("Farms");
        }
    }
}