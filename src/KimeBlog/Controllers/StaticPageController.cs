using KimeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace KimeBlog.Controllers
{
    public class StaticPageController : Controller
    {
        // GET: StaticPage
        public ActionResult Projects()
        {
            BlogPost post = KimeBlogApp.GeneratePost("projects");

            var rex = new Regex(@"\[status:(.*)\]", RegexOptions.Multiline);


            post.ContentHtml = rex.Replace(post.ContentHtml, delegate(Match m)
            {
                //<span class="label label-success">Active</span>
                return @"<span class='label label-success'>" + m.Groups[1].ToString() + "</span>";
            });
            return View("Index", post);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}