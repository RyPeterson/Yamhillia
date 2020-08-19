using YamhilliaNET.Exceptions;

namespace YamhilliaNET.Util.Preconditions
{
    public static class ObjectPreconditions
    {
        public static T ExistsOrNotFound<T>(T nullableValue)
        {
            if (nullableValue == null)
            {
                throw new YamhilliaNotFoundError($"{typeof(T).Name} Not Found");
            }

            return nullableValue;
        }
    }
}