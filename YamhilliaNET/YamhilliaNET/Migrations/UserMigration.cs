using FluentMigrator;
using YamhilliaNET.Migrations.Extensions;

namespace YamhilliaNET.Migrations
{
    [Migration(1)]
    public class UserMigration: Migration
    {
        public override void Up()
        {
            Create
                .Table("Users")
                .WithCommonStructure()
                .WithColumn("Username").AsString().NotNullable().Unique()
                .WithColumn("PasswordHash").AsBinary().NotNullable()
                .WithColumn("PasswordSalt").AsBinary().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}