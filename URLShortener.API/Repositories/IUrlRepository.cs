using URLShortener.API.Entities;

namespace URLShortener.API.Repositories
{
    public interface IUrlRepository
    {
        Task AddUrlAsync(Url url);
        Task<Url> GetUrlAsync(string longUrl);
        Task<bool> SaveChangesAsync();
    }
}
