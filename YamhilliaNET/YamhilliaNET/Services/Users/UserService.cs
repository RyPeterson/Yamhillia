using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models;
using YamhilliaNET.Models.Entities;
using YamhilliaNET.Models.User;
using YamhilliaNET.Util;

namespace YamhilliaNET.Services.Users
{
    
    public class UserService: IUserService
    {
        private readonly YamhilliaContext _context;

        public UserService(YamhilliaContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var userByUsername = await GetUserByUsername(username);
            if (userByUsername == null)
            {
                return null;
            }

            return !PasswordUtil.Verify(password, userByUsername.PasswordHash, userByUsername.PasswordSalt) ? null : userByUsername;
        }

        public async Task<User> CreateUser(CreateUser createUser)
        {
            if (createUser == null)
            {
                throw new ArgumentNullException(nameof(createUser));
            }
            if (string.IsNullOrWhiteSpace(createUser.Username))
            {
                throw new YamhilliaBadRequestError("Username is required");
            }

            if (string.IsNullOrWhiteSpace(createUser.Password))
            {
                throw new YamhilliaBadRequestError("Password is required");
            }

            if (!PasswordUtil.IsStrongEnough(createUser.Password))
            {
                throw new YamhilliaBadRequestError("Password is not strong enough.");
            }

            if (!IsValidEmail(createUser.Username))
            {
                throw new YamhilliaBadRequestError("Invalid email");
            }

            var existing = await GetUserByUsername(createUser.Username);
            if (existing != null)
            {
                throw new YamhilliaBadRequestError("Invalid username");
            }

            PasswordUtil.Hash(createUser.Password, out var passwordHash, out var passwordSalt);
            var untracked = _context.Users.Add(new User()
            {
                Username = createUser.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
            await _context.SaveChangesAsync();
            return untracked.Entity;
        }

        public async Task<User> UpdateUser(UpdateUser updateUser)
        {
            if (updateUser == null)
            {
                throw new ArgumentNullException(nameof(updateUser));
            }
            if (updateUser.Username != null && string.IsNullOrWhiteSpace(updateUser.Username))
            {
                throw new YamhilliaBadRequestError("Username is required");
            }

            if (updateUser.Password != null && string.IsNullOrWhiteSpace(updateUser.Password))
            {
                throw new YamhilliaBadRequestError("Password is required");
            }

            if (updateUser.Password != null && !PasswordUtil.IsStrongEnough(updateUser.Password))
            {
                throw new YamhilliaBadRequestError("Password is not strong enough.");
            }

            if (!IsValidEmail(updateUser.Username))
            {
                throw new YamhilliaBadRequestError("Invalid email");
            }

            var userById = await GetUserById(updateUser.Id);
            if (userById == null)
            {
                throw new YamhilliaNotFoundError("User Not Found");
            }

            var existing = await GetUserByUsername(updateUser.Username);
            if (existing != null && existing.Id != userById.Id)
            {
                throw new YamhilliaBadRequestError("Invalid username");
            }

            userById.Username = updateUser.Username;
            if (updateUser.Password != null)
            {
                PasswordUtil.Hash(updateUser.Password, out var passwordHash, out var passwordSalt);
                userById.PasswordHash = passwordHash;
                userById.PasswordSalt = passwordSalt;
            }

            var updated = _context.Users.Update(userById);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

        public Task<User> GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        private static bool IsValidEmail(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            try
            {
                // I am NOT going to write a fancy check for this...
                new MailAddress(username);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}