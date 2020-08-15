using FluentMigrator.Builders.Create.Table;

namespace YamhilliaNET.Migrations.Extensions
{
    public static class FluentMigratorExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithPrimaryKey(
            this ICreateTableWithColumnSyntax builder)
        {
            return builder.WithColumn("Id").AsInt64().Unique().Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithTimestamps(this ICreateTableWithColumnSyntax builder)
        {
            return builder.WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().NotNullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithUUID(this ICreateTableWithColumnSyntax builder)
        {
            return builder.WithColumn("EntityUUID").AsString().Unique().NotNullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithCommonStructure(this ICreateTableWithColumnSyntax builder)
        {
            return WithUUID(WithTimestamps(WithPrimaryKey(builder)));
        }
    }
}