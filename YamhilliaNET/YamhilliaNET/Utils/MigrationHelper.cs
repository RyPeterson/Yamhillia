using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Utils
{
    public class MigrationHelper
    {
        public static string AutoUpdatingTimestampColumn(MigrationBuilder builder)
        {
            if(builder.IsSqlite())
            {
                return "datetime('now')";
            }
            else 
            {
                return "current_timestamp";
            }
        }
    }
}