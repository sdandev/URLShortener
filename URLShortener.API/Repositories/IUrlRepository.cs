using URLShortener.API.Entities;

namespace URLShortener.API.Repositories
{
    public interface IUrlRepository
    {
        Task AddUrlAsync(Url url);
        Task<Url> GetUrlByLongUrlAsync(string longUrl);
        Task<Url> GetUrlByShortUrlAsync(string shortUrl);
        Task<bool> SaveChangesAsync();
    }
}
