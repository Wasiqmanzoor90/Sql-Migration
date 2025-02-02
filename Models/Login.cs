using System.ComponentModel.DataAnnotations;

namespace Sql_Migration.Models
{
    public class Login
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
