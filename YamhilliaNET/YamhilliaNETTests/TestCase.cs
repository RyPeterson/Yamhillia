using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YamhilliaNET.Data;

namespace YamhilliaNETTests
{
    public class TestCase : IntegrationTestCase
    {
        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        }

    }
}