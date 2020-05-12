using System;
using System.Security.Policy;
using Xunit;
using YamhillaNET.Models;
using YamhillaNET.Models.Entities;
using YamhillaNET.Util;

namespace YamhilliaNETTests.Data
{
    public class YamhilliaContextTests
    {
        [Fact]
        public async void TestSetUp()
        {
            using (var db = new TestDbContext())
            {
                db.Database.EnsureCreated();
                byte[] hash, salt;
                PasswordUtil.Hash("Password", out hash, out salt);
                
                var added = db.Users.Add(new User()
                {
                    Username = "Test",
                    PasswordHash = hash,
                    PasswordSalt = salt
                });
                await db.SaveChangesAsync();
                var user = await db.Users.FindAsync(added.Entity.Id);
                Assert.NotNull(user);
                Assert.NotEqual(user.CreatedAt, DateTime.MinValue);
                Assert.NotEqual(user.UpdatedAt, DateTime.MinValue);
            }
        }
    }
}