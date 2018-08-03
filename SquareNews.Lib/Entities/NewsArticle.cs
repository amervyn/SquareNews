using System;

namespace SquareNews.Lib.Entities
{
    public class NewsArticle
    {
        public int ArticleId { get; set; }
        public int SourceId { get; set; }
        public string NewsApiSourceId { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Country { get; set; }
    }
}