using System;

namespace SquareNews.Lib.Repository
{
    public class NewsSource
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public bool Enabled { get; set; }
        public DateTime LastQueried { get; set; }
    }
}