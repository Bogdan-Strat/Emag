using System.Security.Cryptography;
using System.Text;
using UserService.Entities;

namespace UserService.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public static User FromDTO(RegisterDTO dto)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Role = Role.User,
                Password = HashPassword(dto.Password)
            };
        }

        private static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
