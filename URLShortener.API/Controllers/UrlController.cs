using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using URLShortener.API.Entities;
using URLShortener.API.Models;
using URLShortener.API.Repositories;
using URLShortener.API.Services;
using URLShortener.API.Settings;

namespace URLShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlService _urlService;
        private readonly AppSettings _appSettings;

        //settings prefixes
        //settings baseurl

       // private const string BaseUrl = "www.lupevents.com/";

        public UrlController(IUrlRepository urlRepository, IUrlService urlService, IOptions<AppSettings> appSettings)
        {
            _urlRepository = urlRepository;
            _urlService = urlService;
            _appSettings = appSettings?.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Shorten([FromBody] UrlRequest request)
        {
            // Read the long URL from the request body
            var longUrl = request?.Url;
            if (string.IsNullOrEmpty(longUrl))
                return BadRequest("URL cannot be empty.");

            // Check that the long URL does not start with invalid prefixes
            foreach (var prefix in _appSettings.InvalidPrefixes.Split(','))
            {
                if (longUrl.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return BadRequest("URL cannot start with '" + _appSettings.InvalidPrefixes + "'.");
            }

            // Check if the long URL already exists in the database
            var existingUrl = await _urlRepository.GetUrlByLongUrlAsync(_appSettings.BaseUrl + longUrl);
            if (existingUrl != null)
            {
                // Return the existing short URL in the response body
                var response = new { short_url = existingUrl.ShortUrl };
                return Ok(response);
            }

            // Generate a unique short URL and add it to the database
            var shortUrl = _urlService.GenerateShortUrl(); //make sure uniqueness, to be edited
            await _urlRepository.AddUrlAsync(new Url { ShortUrl = _appSettings.BaseUrl + shortUrl, LongUrl = _appSettings.BaseUrl + longUrl });
            await _urlRepository.SaveChangesAsync();

            // Return the shortened URL in the response body
            var newResponse = new { short_url = _appSettings.BaseUrl + shortUrl };
            return Ok(newResponse);
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectResult(string shortUrl)
        {
            string decodedUrl = System.Net.WebUtility.UrlDecode(shortUrl);

            // Retrieve the URL from the database using the short URL as the key
            var url = await _urlRepository.GetUrlByShortUrlAsync(decodedUrl);
            if (url != null)
                // Redirect to the original URL
                return Redirect(url.LongUrl);

            // If the short URL is not found in the database, return a 404 error
            return NotFound();
        }

    }
}
