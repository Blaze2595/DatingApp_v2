using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Repositories.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext Context { get; }
        public AuthRepository(DataContext context)
        {
            this.Context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username);

            if (user != null)
            {
                if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return user;
                }
            }

            return null;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await Context.Users.AnyAsync(x => x.Username == username))
                return true;
            else
                return false;
        }
    }
}