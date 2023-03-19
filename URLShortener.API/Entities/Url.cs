using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortener.API.Entities
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
    }
}
