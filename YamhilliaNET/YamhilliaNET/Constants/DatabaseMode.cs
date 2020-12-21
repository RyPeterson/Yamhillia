using System;
using YamhilliaNET.Util;

namespace YamhilliaNET.Constants
{
    public class DatabaseMode: StringEnumeration
    {
        private DatabaseMode(string value) : base(value)
        {
        }
        
        public static readonly DatabaseMode POSTGRES = new DatabaseMode("POSTGRES");
        public static readonly DatabaseMode SQLITE = new DatabaseMode("SQLITE");

        public static DatabaseMode FromString(string value)
        {
            if (POSTGRES.Value ==  value)
            {
                return POSTGRES;
            }

            if (SQLITE.Value == value)
            {
                return SQLITE;
            }
            throw new ArgumentException("value is not a valid DatabaseMode", value);
        }
    }
}