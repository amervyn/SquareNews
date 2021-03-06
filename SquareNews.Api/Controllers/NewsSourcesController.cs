﻿using SquareNews.Lib.Entities;
using SquareNews.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SquareNews.Api.Controllers
{
    public class NewsSourcesController : ApiController
    {
        private NewsApiSourceRepository _sourceRepository;


        public NewsSourcesController()
        {
            _sourceRepository = new NewsApiSourceRepository();
        }


        // GET: api/NewsSources
        public IHttpActionResult Get()
        {
            //var result = new List<NewsApiSource>();

            var result = new List<string>();

            //result = _sourceRepository.GetAll(DateTime.MinValue, 0);

            result = _sourceRepository.RunSqlCommand() as List<string>;

            if(result!=null)
                return Json(result);

            return Json(new List<string>());
        }

        // GET: api/NewsSources/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NewsSources
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NewsSources/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NewsSources/5
        public void Delete(int id)
        {
        }
    }
}
