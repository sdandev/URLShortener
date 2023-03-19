using Microsoft.EntityFrameworkCore;
using URLShortener.API.DbContexts;
using URLShortener.API.Entities;

namespace URLShortener.API.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlDbContext _urlDbContext;

        public UrlRepository(UrlDbContext urlDbContext)
        {
            _urlDbContext = urlDbContext ?? throw new ArgumentNullException(nameof(urlDbContext));
        }

        public async Task AddUrlAsync(Url url)
        {
            await _urlDbContext.Urls.AddAsync(url);
        }

        public async Task<Url> GetUrlAsync(string longUrl)
        {
            return _urlDbContext.Urls.SingleOrDefaultAsync(u => u.LongUrl == longUrl).Result;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _urlDbContext.SaveChangesAsync() >= 0);
        }
    }
}
