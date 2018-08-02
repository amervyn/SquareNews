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
using SquareNews.Lib.Repository;
using SquareNews.Lib.Entities;

namespace SquareNews.Lib.Aggregation
{
    public class PublicApiCaller : IPublicApiCaller
    {
        private IDataRepository<NewsSource> _dataRepository;


        public PublicApiCaller()
        {
            _dataRepository = new SqlRepository<NewsSource>();
        }
        public async Task<bool> CallPublicService()
        {
            await CallNewsApi();

            return true;
        }

        private async Task<bool> CallNewsApi()
        {
            var newssources = "https://newsapi.org/v2/sources?language=en&apiKey=" + "5e7564559c884718a1a1cd8955d0f767";

            var client = new WebClient().DownloadString(newssources);

            var localSource = _dataRepository.GetByKey("1");

            if (localSource == null)
                return false;

            // init with API key
            var newsApiClient = new NewsApiClient(localSource.ApiKey);  //("5e7564559c884718a1a1cd8955d0f767");
            var articlesResponse = new ArticlesResult();


            await Task.Run(() => articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = new List<string>(), //get from db
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.AddMinutes(-30),
                PageSize = 100
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

                return true;
            }

            return false;
        }
    }
}
