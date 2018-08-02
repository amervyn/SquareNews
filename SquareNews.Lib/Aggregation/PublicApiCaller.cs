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
using Newtonsoft.Json;
using SquareNews.Lib.Objects;

namespace SquareNews.Lib.Aggregation
{
    public class PublicApiCaller : IPublicApiCaller
    {
        private IDataRepository<NewsSource> _newsSourceRepository;
        private IDataRepository<NewsApiSource> _newsApiSourceRepository;


        public PublicApiCaller()
        {
            _newsApiSourceRepository = new NewsApiSourceRepository();
            _newsSourceRepository = new NewsSourceRepository();
        }
        public async Task<bool> CallPublicService()
        {
            await UpdateNewsApiSources();

            await CallNewsApi();

            return true;
        }

        private async Task<bool> UpdateNewsApiSources()
        {
            var newssources = "https://newsapi.org/v2/sources?apiKey=" + "5e7564559c884718a1a1cd8955d0f767";

            var client = new WebClient();
            var result = await client.DownloadStringTaskAsync(new Uri(newssources));

            if (result != null)
            {
                var json = JsonConvert.DeserializeObject<NewsApiSourceObject>(result);
                if (json != null && json.Status == "ok")
                {
                    foreach (var s in json.Sources)
                    {
                        var newsApiSource = new NewsApiSource
                        {
                            Name = s.Name,
                            ApiSourceName = s.Id,
                            Category = s.Category,
                            Country = s.Country,
                            Description = s.Description,
                            Language = s.Language,
                            Url = s.Url,
                            Enabled = true
                        };

                        _newsApiSourceRepository.Create(newsApiSource);
                    }
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> CallNewsApi()
        {
            var localSource = _newsSourceRepository.GetByKey("1"); //newsapi source

            if (localSource == null)
                return false;

            // init with API key
            var newsApiClient = new NewsApiClient(localSource.ApiKey);  //("5e7564559c884718a1a1cd8955d0f767");
            var articlesResponse = new ArticlesResult();


            await Task.Run(() => articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = _newsApiSourceRepository.GetAll().Select(c => c.ApiSourceName).ToList(), //get from db
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.AddMinutes(-30),
                PageSize = 100
            }));


            if (articlesResponse.Status == Statuses.Ok)
            {

            }

            return false;
        }
    }
}
