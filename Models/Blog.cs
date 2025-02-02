namespace Sql_Migration.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public required string  Tittle { get; set; }
        public required string  Content { get; set; }
        public  string ? Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? ImageUrl { get; set; }
  

    }
}
