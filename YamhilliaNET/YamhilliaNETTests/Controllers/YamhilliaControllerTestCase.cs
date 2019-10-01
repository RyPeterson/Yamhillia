using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using YamhilliaNET.Controllers;

namespace YamhilliaNETTests.Controllers
{
    public class YamhilliaControllerTestCase : IntegrationTestCase
    {
        private readonly YamhilliaController controller;
        public YamhilliaControllerTestCase()
        {
            controller = new YamhilliaController();
        }

        [Fact]
        public void TestPing()
        {
            Assert.Equal(200, controller.Ping());
        }
    }
}
