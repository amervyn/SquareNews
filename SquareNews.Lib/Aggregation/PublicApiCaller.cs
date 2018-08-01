using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Aylien.NewsApi;
using System.Diagnostics;
using SquareNews.Lib.Interface;
using Dapper;
using SquareNews.Lib.Database;

namespace SquareNews.Lib.Aggregation
{
    public class PublicApiCaller : IPublicApiCaller
    {
        private DbFactory _dbFactory;


        public PublicApiCaller()
        {
            _dbFactory = new SqlDbFactory();
        }
        public async Task<string> CallPublicService()
        {
            //var url = "https://newsapi.org/v2/top-headlines?" + "country=us&" + "apiKey=5e7564559c884718a1a1cd8955d0f767";

            //var client = new WebClient().DownloadString(url);

            // init with your API key
            var newsApiClient = new NewsApiClient("5e7564559c884718a1a1cd8955d0f767");
            var articlesResponse = new ArticlesResult();


            await Task.Run(() => articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = new List<string>(), //get from db
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Today.AddDays(-2)
            }));

            var sb = new StringBuilder();

            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                //Debug.WriteLine(articlesResponse.TotalResults);
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    // title
                    //Debug.WriteLine(article.Title);
                    sb.AppendLine(article.Title);
                    // author
                    //Debug.WriteLine(article.Author);
                    sb.AppendLine(article.Author);
                    // description
                    //Debug.WriteLine(article.Description);
                    sb.AppendLine(article.Description);
                    // url
                    //Debug.WriteLine(article.Url);
                    sb.AppendLine(article.Url);
                    // image
                    //Debug.WriteLine(article.UrlToImage);
                    sb.AppendLine(article.UrlToImage);
                    // published at
                    //Debug.WriteLine(article.PublishedAt);
                    sb.AppendLine(article.PublishedAt.ToString());
                }

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}
