using System;
using System.Collections.Generic;

namespace LinkShortener.Persistence.Database.ShortenedLink
{
    public partial class ShortenedLink
    {
        public int Id { get; set; }
        public string? OriginalUrl { get; set; }
        public string? ShortUrl { get; set; }
    }
}
