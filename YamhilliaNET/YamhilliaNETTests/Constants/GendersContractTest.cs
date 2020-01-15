using System;
using Xunit;
using YamhilliaNET.Constants;

namespace YamhilliaNETTests.Constants
{
    /// <summary>
    /// Verifies order of enums don't change.await.
    /// If these fail, fix the code, not the test
    /// </summary>
    public class GendersContractTest
    {
        [Fact]
        public void TestOrderDoesntChange()
        {
            Assert.Equal(Genders.Male, Enum.Parse<Genders>("0"));
            Assert.Equal(Genders.Female, Enum.Parse<Genders>("1"));
            Assert.Equal(Genders.Other, Enum.Parse<Genders>("2"));
            Assert.Equal(Genders.Neutered, Enum.Parse<Genders>("3"));
        }
    }
}