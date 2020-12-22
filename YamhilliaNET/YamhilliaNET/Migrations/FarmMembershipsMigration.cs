using System.Data;
using FluentMigrator;
using YamhilliaNET.Migrations.Extensions;

namespace YamhilliaNET.Migrations
{
    [Migration(3)]
    public class FarmMembershipsMigration: Migration
    {
        public override void Up()
        {
            Create.Table("FarmMemberships")
                .WithCommonStructure()
                .WithColumn("UserId")
                    .AsInt64()
                    .Indexed()
                    .NotNullable()
                .WithColumn("FarmId")
                    .AsInt64()
                    .Indexed()
                    .NotNullable()
                .WithColumn("MemberType")
                    .AsString()
                    .NotNullable();
            Create.ForeignKey()
                .FromTable("FarmMemberships")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("FarmMemberships")
                .ForeignColumn("FarmId")
                .ToTable("Farms")
                .PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    
        public override void Down()
        {
            Delete.Table("FarmMemberships");
        }
    }
}