using Microsoft.EntityFrameworkCore;
using Sql_Migration.Models;

namespace Sql_Migration.Data
{
    public class SqlDbContext: DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options):base(options) 
        {

        }
        public DbSet<User> User2 { get; set; }
    }
}
