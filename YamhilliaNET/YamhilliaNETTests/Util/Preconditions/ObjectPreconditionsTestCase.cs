using Xunit;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models.Entities;
using YamhilliaNET.Util.Preconditions;

namespace YamhilliaNETTests.Util.Preconditions
{
    public class ObjectPreconditionsTestCase
    {
        [Fact]
        public void Test_ExistsOrIsNotFound()
        {
            Assert.Equal("Test", ObjectPreconditions.ExistsOrNotFound("Test"));
            Assert.Equal(1, ObjectPreconditions.ExistsOrNotFound<int?>(1));
            Assert.Throws<YamhilliaNotFoundError>(() => ObjectPreconditions.ExistsOrNotFound<string>(null));
            Assert.Throws<YamhilliaNotFoundError>(() => ObjectPreconditions.ExistsOrNotFound<int?>(null));
            try
            {
                ObjectPreconditions.ExistsOrNotFound<string>(null);
            }
            catch (YamhilliaNotFoundError e)
            {
                Assert.Equal("String Not Found", e.Message);
            }
            
            try
            {
                ObjectPreconditions.ExistsOrNotFound<object>(null);
            }
            catch (YamhilliaNotFoundError e)
            {
                Assert.Equal("Object Not Found", e.Message);
            }
            
            try
            {
                ObjectPreconditions.ExistsOrNotFound<User>(null);
            }
            catch (YamhilliaNotFoundError e)
            {
                Assert.Equal("User Not Found", e.Message);
            }
        }
    }
}