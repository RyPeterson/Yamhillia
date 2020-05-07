using Xunit;
using YamhillaNET.Util;

namespace YamhilliaNETTests.Util
{
    public class StringEnumerationTestCase
    {
        [Fact]
        public void TestCompares()
        {
            Assert.False(null == TestEnumeration.BAR);
            Assert.False(TestEnumeration.BAR == null);
            Assert.False(TestEnumeration.FOO == TestEnumeration.BAR);
            Assert.False(TestEnumeration.BAR == TestEnumeration.FOO);
            Assert.True(TestEnumeration.BAR == TestEnumeration.BAR);
            Assert.True(TestEnumeration.FOO == TestEnumeration.FOO);
            
            Assert.True(null != TestEnumeration.BAR);
            Assert.True(TestEnumeration.BAR != null);
            Assert.True(TestEnumeration.FOO != TestEnumeration.BAR);
            Assert.True(TestEnumeration.BAR != TestEnumeration.FOO);
            Assert.False(TestEnumeration.BAR != TestEnumeration.BAR);
            Assert.False(TestEnumeration.FOO != TestEnumeration.FOO);
        }
    }

    public class TestEnumeration : StringEnumeration
    {
        private TestEnumeration(string value) : base(value)
        {
        }
        
        
        public static TestEnumeration FOO = new TestEnumeration("Foo");
        public static TestEnumeration BAR = new TestEnumeration("BAR");
        
    }
}