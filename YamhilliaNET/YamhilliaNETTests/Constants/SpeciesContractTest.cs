using System;
using System.Linq;
using Xunit;
using YamhilliaNET.Constants;

namespace YamhilliaNETTests.Constants
{
    /// <summary>
    /// Verifies order of enums don't change.await.
    /// If these fail, fix the code, not the test
    /// </summary>
    public class SpeciesContractTest
    {
        [Fact]
        public void TestOrderDoesntChange()
        {
            Assert.Equal(Species.Chicken, Enum.Parse<Species>("0"));
            Assert.Equal(Species.Cow, Enum.Parse<Species>("1"));
            Assert.Equal(Species.Duck, Enum.Parse<Species>("2"));
            Assert.Equal(Species.Goose, Enum.Parse<Species>("3"));
            Assert.Equal(Species.Goat, Enum.Parse<Species>("4"));
            Assert.Equal(Species.Horse, Enum.Parse<Species>("5"));
            Assert.Equal(Species.Llama, Enum.Parse<Species>("6"));
            Assert.Equal(Species.Pig, Enum.Parse<Species>("7"));
            Assert.Equal(Species.Rabbit, Enum.Parse<Species>("8"));
            Assert.Equal(Species.Turkey, Enum.Parse<Species>("9"));

            //Make sure no changes get past this.
            Assert.Equal(10, Enum.GetValues(typeof(Species)).Cast<Species>().Count());
        }
    }
}