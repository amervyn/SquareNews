using Newtonsoft.Json;
using SquareNews.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace SquareNews.Web.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://192.168.95.1:5555/";
        public async Task<ActionResult> Index()
        {
            var result = new ArticleResult();

            var api = new RestClient("http://amervyn.duckdns.org/");

            var fromDate = new Parameter
            {
                Type = ParameterType.QueryString,
                Name = "fromDate",
                Value = DateTime.Now.AddHours(-6).ToString("yyyy-MM-ddTHH:mm:ss")
            };

            var pageNumber = new Parameter
            {
                Type = ParameterType.QueryString,
                Name = "page",
                Value = 1
            };

            var pageSize = new Parameter
            {
                Type = ParameterType.QueryString,
                Name = "pageSize",
                Value = 50
            };

            var request = new RestRequest("api/Articles", Method.GET);

            request.AddParameter(fromDate);
            request.AddParameter(pageNumber);
            request.AddParameter(pageSize);

            var queryResult = api.Execute<ArticleResult>(request).Data;

            //using (var client = new HttpClient())
            //{
            //    //Passing service base url  
            //    client.BaseAddress = new Uri(Baseurl);

            //    client.DefaultRequestHeaders.Clear();
            //    //Define request data format  
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
            //    HttpResponseMessage Res = await client.GetAsync("http://amervyn.duckdns.org/api/Articles/?fromDate=2018-08-06T13:00:00");

            //    //Checking the response is successful or not which is sent using HttpClient  
            //    if (Res.IsSuccessStatusCode)
            //    {
            //        //Storing the response details recieved from web api   
            //        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

            //        //Deserializing the response recieved from web api and storing into the Employee list  
            //        result = JsonConvert.DeserializeObject<ArticleResult>(EmpResponse);

            //    }
            //    //returning the employee list to view  

            //}

            return View(queryResult);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}