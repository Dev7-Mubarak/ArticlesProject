using ArticlesProject.Core;
using Microsoft.EntityFrameworkCore;

namespace ArticlesProject.Data.SqlServerEF
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-HQ6JRH1;Database=ArticlesProjectDB;User Id=sa;Password=123;Trusted_Connection=True;TrustServerCertificate = True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorPost> AuthorPosts { get; set; }
    }
}
