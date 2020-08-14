using System;
using YamhilliaNET.Util;

namespace YamhilliaNET.Constants
{
    public class DatabaseMode: StringEnumeration
    {
        private DatabaseMode(string value) : base(value)
        {
        }
        
        public static DatabaseMode POSTGRES = new DatabaseMode("POSTGRES");
        public static DatabaseMode SQLITE = new DatabaseMode("SQLITE");

        public static DatabaseMode FromString(string value)
        {
            if (DatabaseMode.POSTGRES.Value ==  value)
            {
                return DatabaseMode.POSTGRES;
            }

            if (DatabaseMode.SQLITE.Value == value)
            {
                return DatabaseMode.SQLITE;
            }
            throw new ArgumentException("value is not a valid DatabaseMode", value);
        }
    }
}