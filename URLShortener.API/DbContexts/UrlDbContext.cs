using Microsoft.EntityFrameworkCore;
using URLShortener.API.Entities;

namespace URLShortener.API.DbContexts
{
    public class UrlDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; } = null!;

        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
        {

        }
    }
}
