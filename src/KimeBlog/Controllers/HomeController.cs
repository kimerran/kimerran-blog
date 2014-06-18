using KimeBlog.App_Start;
using KimeBlog.Models;
using MarkdownSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace KimeBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // read all.json
            string alljson = System.IO.File.ReadAllText(BlogConfig.PATH_POSTS + "all.json");

            List<BlogPost> posts = (JsonConvert.DeserializeObject<List<BlogPost>>(alljson));

            posts.RemoveAll(o => o.Tags.Contains("archive"));
           

            return View(posts.OrderByDescending(o => o.Id).Take(12).ToList());
        }
    }
}