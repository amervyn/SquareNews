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

namespace SquareNews.Api.Controllers
{
    public class ArticlesController : ApiController
    {
        private ArticleRepository _articleRepository;

        public ArticlesController()
        {
            _articleRepository = new ArticleRepository();
        }


        // GET api/<controller>
        public IHttpActionResult GetAll()
        {
            var resultsToShow = -1;
            var pageNumber = -1;
            var fromDate = new DateTime(2018, 1, 1);

            var pageSize = Request.GetQueryString("pageSize");
            var page = Request.GetQueryString("page");
            var from = Request.GetQueryString("fromDate");

            if (!string.IsNullOrEmpty(pageSize))
                resultsToShow = Convert.ToInt16(pageSize);

            if (!string.IsNullOrEmpty(page))
                pageNumber = Convert.ToInt16(page);

            if (!string.IsNullOrEmpty(from))
                fromDate = Convert.ToDateTime(from);

            var result = new ArticleResult
            {
                Articles = _articleRepository.GetAll(fromDate, resultsToShow).OrderByDescending(c => c.CreatedOn).ToList()
            };

            result.TotalResults = result.Articles.Count();

            if (resultsToShow >= 0)
            {
                result.Articles = result.Articles.Take(resultsToShow).ToList();

                if (pageNumber >= 0)
                    result.Articles = result.Articles.Skip(resultsToShow * pageNumber).Take(resultsToShow).ToList();

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