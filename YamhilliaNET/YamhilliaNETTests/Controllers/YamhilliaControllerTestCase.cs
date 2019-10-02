using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using YamhilliaNET.Controllers;
using YamhilliaNET.Data.Providers;

namespace YamhilliaNETTests.Controllers
{
    public class YamhilliaControllerTestCase : IntegrationTestCase
    {
        private readonly YamhilliaController controller;
        public YamhilliaControllerTestCase()
        {
            
            controller = new YamhilliaController(new TestDatabaseProviders());
        }

        [Fact]
        public async void TestPing()
        {
            Assert.Equal(200, await controller.Ping());
        }
    }
}
