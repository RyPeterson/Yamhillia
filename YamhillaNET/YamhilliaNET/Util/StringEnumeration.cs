namespace YamhillaNET.Util
{
    public class StringEnumeration
    {
        private readonly string _value;
        
        public string Value
        {
            get { return _value; }
        }

        protected StringEnumeration(string value)
        {
            _value = value;
        }

        protected bool Equals(StringEnumeration other)
        {
            return this._value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StringEnumeration) obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(StringEnumeration a, StringEnumeration b)
        {
            if (ReferenceEquals(null, a))
            {
                return ReferenceEquals(null, b);
            }

            if (ReferenceEquals(null, b))
            {
                return ReferenceEquals(null, a);
            }
            return a != null && a.Equals(b);
        }

        public static bool operator !=(StringEnumeration a, StringEnumeration b)
        {
            return !(a == b);
        }
    }
}