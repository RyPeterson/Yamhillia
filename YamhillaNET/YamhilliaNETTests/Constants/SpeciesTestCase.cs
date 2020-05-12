using System.Globalization;
using Xunit;
using YamhillaNET.Constants;
using YamhillaNET.Exceptions;

namespace YamhilliaNETTests.Constants
{
    public class SpeciesTestCase
    {
        [Fact]
        public void Test_ValueOf()
        {
            Assert.Throws<YamhilliaNotFoundException>(() => Species.ValueOf("Nope"));
            Assert.Throws<YamhilliaNotFoundException>(() => Species.ValueOf(""));
            Assert.Throws<YamhilliaNotFoundException>(() => Species.ValueOf(null));
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (var species in Species.SpeciesList)
            {
                Assert.Equal(species, Species.ValueOf(species.Value));
                Assert.Equal(species, Species.ValueOf(species.Value.ToLower()));
                Assert.Equal(species, Species.ValueOf(species.Value.ToUpper()));
                Assert.Equal(species, Species.ValueOf(textInfo.ToTitleCase(species.Value)));
            }
        }
    }
}