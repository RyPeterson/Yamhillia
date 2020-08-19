using FluentMigrator;
using YamhilliaNET.Migrations.Extensions;

namespace YamhilliaNET.Migrations
{
    [Migration(3)]
    public class AssignedFarmMigration: Migration
    {
        public override void Up()
        {
            Alter.Table("Users").AddColumn("FarmId").AsInt64().Nullable().WithDefaultValue(null);
            Create.ForeignKey().FromTable("Users").ForeignColumn("FarmId").ToTable("Farms").PrimaryColumn("Id");
        }
    
        public override void Down()
        {
            Delete.Column("FarmId").FromTable("Users");
        }
    }
}