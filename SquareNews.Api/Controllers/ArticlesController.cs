using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SquareNews.Lib.Entities;
using SquareNews.Lib.Repository;
using SquareNews.Api.Models;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Text;
using SquareNews.Api.Services;

namespace SquareNews.Api.Controllers
{
    public class ArticlesController : ApiController
    {
        private ArticleRepository _articleRepository;
        private ArticleResultRepository _articleResultRepository;

        public ArticlesController()
        {
            _articleRepository = new ArticleRepository();
            _articleResultRepository = new ArticleResultRepository();
        }


        // GET api/<controller>
        public IHttpActionResult GetAll()
        {
            var resultsToShow = 20;
            var pageNumber = 1;
            var fromDate = new DateTime(2018, 1, 1);
            var country = "gb";

            var pageSize = Request.GetQueryString("pageSize");
            var page = Request.GetQueryString("page");
            var from = Request.GetQueryString("fromDate");
            var countryQs = Request.GetQueryString("country");

            if (!string.IsNullOrEmpty(pageSize))
                resultsToShow = Convert.ToInt16(pageSize);

            if (!string.IsNullOrEmpty(page))
                pageNumber = Convert.ToInt16(page);

            if (!string.IsNullOrEmpty(from))
                fromDate = Convert.ToDateTime(from);

            if (!string.IsNullOrEmpty(countryQs))
                country = countryQs;


            var rowStart = 1 + (pageNumber * resultsToShow) - resultsToShow;

            var a = _articleResultRepository.GetAll(fromDate, resultsToShow, country, rowStart);

            var result = new ArticleResult();

            if (a.Any())
            {
                result.TotalResults = a[0].TotalResults;
                result.Articles = a[0].Articles.ToList();
            }


            return Json(result);
        }

        // GET api/<controller>/5
        public string GetById(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}