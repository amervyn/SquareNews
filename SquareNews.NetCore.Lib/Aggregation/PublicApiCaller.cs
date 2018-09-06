using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Diagnostics;
using SquareNews.Lib.Interface;
using Dapper;
using SquareNews.Lib.Database;
using SquareNews.Lib.Repository;
using SquareNews.Lib.Entities;
using Newtonsoft.Json;
using SquareNews.Lib.Objects;
using NLog;

namespace SquareNews.Lib.Aggregation
{
    public class PublicApiCaller : IPublicApiCaller
    {
        private IDataRepository<NewsSource> _newsSourceRepository;
        private IDataRepository<NewsApiSource> _newsApiSourceRepository;
        private IDataRepository<NewsArticle> _articleRepository;

        private NewsApiClient _newsApiClient;
        private List<NewsArticle> _newsArticles;
        private int _resultsRemaining = 0;
        private int _newsApiPage = 1;
        private Countries _newsApiCountry=Countries.GB;
        private string _apiLookupKey = string.Empty;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public PublicApiCaller()
        {
            _logger.Info("Init Api Caller");
            _newsApiSourceRepository = new NewsApiSourceRepository();
            _newsSourceRepository = new NewsSourceRepository();
            _articleRepository = new ArticleRepository();
            _newsArticles = new List<NewsArticle>();
        }
        public async Task<bool> CallPublicService()
        {
            _logger.Info("Calling public api services");

            switch(_apiLookupKey)
            {
                case "1":
                    _apiLookupKey = "2";
                    break;
                case "2":
                    _apiLookupKey = "1002";
                    break;
                case "1002":
                    _apiLookupKey = "1";
                        break;
                default:
                    _apiLookupKey = "1";
                    break;
            }

            await UpdateNewsApiSources();

            await CallNewsApiHeadlines();

            await CallNewsApiEverything();

            return true;
        }

        private async Task<bool> UpdateNewsApiSources()
        {
            var localSource = _newsSourceRepository.GetByKey(_apiLookupKey); //newsapi source

            if (localSource == null)
                return false;

            _logger.Info("Using APIKEY: " + localSource.ApiKey);

            var newssources = "https://newsapi.org/v2/sources?apiKey=" + localSource.ApiKey; //"7ccec5269a994497a486934b8fa1009d";//"5e7564559c884718a1a1cd8955d0f767";

            var client = new WebClient();

            var result = await client.DownloadStringTaskAsync(new Uri(newssources));

            if (result != null)
            {
                _logger.Info("Got NewsApi source");

                var json = JsonConvert.DeserializeObject<NewsApiSourceObject>(result);
                if (json != null && json.Status == "ok")
                {
                    foreach (var s in json.Sources)
                    {
                        _logger.Info("Updating source: " + s.Name);

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

        private async Task<bool> CallNewsApiHeadlines()
        {
            _logger.Info("-----------------------------");
            _logger.Info("Calling NewsApi Headlines");
            _logger.Info("-----------------------------");

            var localSource = _newsSourceRepository.GetByKey(_apiLookupKey); //newsapi source

            if (localSource == null)
                return false;

            // init with API key
            _newsApiClient = new NewsApiClient(localSource.ApiKey);  //("5e7564559c884718a1a1cd8955d0f767");

            foreach (var c in Enum.GetValues(typeof(Countries)).Cast<Countries>().ToList())
            {
                _newsApiCountry = c;

                await QueryNewsApiHeadlines(100);

                while (_resultsRemaining > 0)
                {
                    _logger.Info("Results remaining: " + _resultsRemaining);
                    _newsApiPage++;
                    await QueryNewsApiHeadlines(100);
                }

            }

            if (_newsArticles.Any())
            {
                _logger.Info("Writing articles");
                foreach (var a in _newsArticles)
                {
                    _logger.Info(a.Headline);
                    _articleRepository.Create(a);
                }

                _newsArticles.Clear();
                _newsApiPage = 1;

                return true;
            }

            return false;
        }


        private async Task<bool> CallNewsApiEverything()
        {
            _logger.Info("-----------------------------");
            _logger.Info("Calling NewsApi Everything");
            _logger.Info("-----------------------------");

            var localSource = _newsSourceRepository.GetByKey(_apiLookupKey); //newsapi source

            if (localSource == null)
                return false;

            // init with API key
            _newsApiClient = new NewsApiClient(localSource.ApiKey);  //("5e7564559c884718a1a1cd8955d0f767");

            await QueryNewsApiEverything(100);

            while (_resultsRemaining > 0)
            {
                _logger.Info("Results remaining: " + _resultsRemaining);
                _newsApiPage++;
                await QueryNewsApiEverything(100);
            }

            if (_newsArticles.Any())
            {
                _logger.Info("Writing articles");
                foreach (var a in _newsArticles)
                {
                    _logger.Info(a.Headline);
                    _articleRepository.Create(a);
                }

                _newsArticles.Clear();
                _newsApiPage = 1;

                return true;
            }

            return false;
        }


        private async Task<bool> QueryNewsApiHeadlines(int pageSize)
        {
            var response = new ArticlesResult();

            _logger.Info("Querying NewsApi. Page: " + _newsApiPage);

            await Task.Run(() => response = _newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
            {
                //Sources = _newsApiSourceRepository.GetAll(new DateTime(2018, 1, 1), 0).Where(c => c.Language == "en").Select(c => c.ApiSourceName).ToList(), //get from db
                //SortBy = SortBys.Popularity,
                //Language = Languages.EN,
                //From = DateTime.Now.AddHours(-1),
                PageSize = pageSize,
                Country = _newsApiCountry,
                Page = _newsApiPage
            }));

            if (response.Status == Statuses.Ok)
            {
                _resultsRemaining = response.TotalResults - (_newsApiPage * pageSize);
                
                foreach (var a in response.Articles)
                {
                    var article = new NewsArticle
                    {
                        Headline = a.Title,
                        SourceId = 1,
                        NewsApiSourceId = a.Source.Id,                        
                        Description = a.Description,
                        IsVisible = true,
                        ImageUrl = a.UrlToImage,
                        Url = a.Url,
                        CreatedOn = DateTime.Now,
                        Country=_newsApiCountry.ToString().ToLower(),
                        PublishedOn = a.PublishedAt ?? DateTime.Now
                    };

                    _logger.Info("Adding article: " + a.Title + " from Source " + a.Source.Name);

                    _newsArticles.Add(article);
                }

                return true;
            }

            return false;
        }


        private async Task<bool> QueryNewsApiEverything(int pageSize)
        {
            var response = new ArticlesResult();

            _logger.Info("Querying NewsApi. Page: " + _newsApiPage);

            await Task.Run(() => response = _newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = _newsApiSourceRepository.GetAll(new DateTime(2018, 1, 1), 0).Select(c => c.ApiSourceName).ToList(), //get from db
                SortBy = SortBys.Popularity,
                //Language = Languages.EN,
                From = DateTime.Now.AddMinutes(-30),
                PageSize = pageSize,
                //Country = _newsApiCountry,
                Page = _newsApiPage
            }));

            if (response.Status == Statuses.Ok)
            {
                _resultsRemaining = response.TotalResults - (_newsApiPage * pageSize);

                foreach (var a in response.Articles)
                {
                    var article = new NewsArticle
                    {                        
                        Headline = a.Title,
                        SourceId = 1,
                        NewsApiSourceId = a.Source.Id,
                        Description = a.Description,
                        IsVisible = true,
                        ImageUrl = a.UrlToImage,
                        Url = a.Url,
                        CreatedOn = DateTime.Now,
                        PublishedOn = a.PublishedAt.HasValue ? a.PublishedAt.Value : DateTime.Now
                    };

                    _logger.Info("Adding article: " + a.Title + " from Source " + a.Source.Name);

                    _newsArticles.Add(article);
                }

                return true;
            }

            return false;
        }
    }
}
