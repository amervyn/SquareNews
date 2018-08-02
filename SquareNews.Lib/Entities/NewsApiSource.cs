using System;
using System.Collections.Generic;

namespace SquareNews.Lib.Entities
{
    public class NewsApiSource
    {
        public int ApiSourceId { get; set; }
        public string ApiSourceName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public bool Enabled { get; set; }
    }
}