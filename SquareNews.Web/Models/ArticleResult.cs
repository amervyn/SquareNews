using System.Collections.Generic;
using System.Linq;
using System.Web;
using SquareNews.Lib.Entities;

namespace SquareNews.Web.Models
{
    public class ArticleResult
    {
        public int TotalResults { get; set; }
        public List<NewsArticle> Articles { get; set; }
    }
}