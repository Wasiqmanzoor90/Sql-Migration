using System.ComponentModel.DataAnnotations;

namespace Sql_Migration.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
