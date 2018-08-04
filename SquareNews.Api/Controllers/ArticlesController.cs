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
            var resultCount = -1;
            var pageNumber = -1;

            var pageSize = Request.GetQueryString("pageSize");
            var page = Request.GetQueryString("page");

            if (!string.IsNullOrEmpty(pageSize))
                resultCount = Convert.ToInt16(pageSize);

            if (!string.IsNullOrEmpty(page))
                pageNumber = Convert.ToInt16(page);

            var result = new ArticleResult
            {
                Articles = _articleRepository.GetAll().OrderByDescending(c => c.PublishedOn).ToList()
            };

            result.TotalResults = result.Articles.Count();


            if (resultCount >= 0)
                result.Articles = result.Articles.Take(resultCount).ToList();


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