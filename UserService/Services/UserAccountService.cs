using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserService.Data;
using UserService.DTOs;
using UserService.Entities;

namespace UserService.Services
{
    public class UserAccountService
    {
        private readonly UserDbContext _context;

        public UserAccountService(UserDbContext context)
        {
            _context = context;
        }

        public async Task Register(RegisterDTO dto)
        {
            var user = RegisterDTO.FromDTO(dto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Login(LoginDTO dto)
        {
            var hashedPassword = HashPassword(dto.Password);

            var user = await _context.Users
                .Where(u => u.Email == dto.Email && u.Password == hashedPassword)
                .SingleOrDefaultAsync();

            return user;
        }

        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

    }
}
