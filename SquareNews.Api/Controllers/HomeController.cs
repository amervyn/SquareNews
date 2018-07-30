using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SquareNews.Lib.Aggregation;

namespace SquareNews.Api.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";
            PublicApiCaller publicApiCaller = new PublicApiCaller();
            var result=await publicApiCaller.CallPublicService();
            return View();
        }
    }
}
