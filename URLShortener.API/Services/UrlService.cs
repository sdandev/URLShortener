namespace URLShortener.API.Services
{
    public class UrlService : IUrlService
    {
        public string GenerateShortUrl()
        {
            // Generate a unique short URL by generating a random string
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var shortUrl = new string(
                new char[7].Select(_ => chars[random.Next(chars.Length)]).ToArray());
            return shortUrl;
        }
    }
}
