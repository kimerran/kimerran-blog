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


            // for the [status:] tags
            var rex = new Regex(@"\[status:(.*)\]", RegexOptions.Multiline);
            post.ContentHtml = rex.Replace(post.ContentHtml, delegate(Match m)
            {
                string class_attrib = ""; 
                switch ( m.Groups[1].ToString().ToLower())
                {
                    case "active":
                        class_attrib = "label-success";
                        break;
                    case "idea":
                        class_attrib = "label-info";
                        break;
                    case "inactive":
                        class_attrib = "label-default";
                        break;
                    default:
                        class_attrib = "label-default";
                        break;
                }


                return @"<span class='label " + class_attrib  + "'>" + m.Groups[1].ToString() + "</span>";
            });

            // for the [tech:] tags
            var rex2 = new Regex(@"\[tech:(.*)\]", RegexOptions.Multiline);
            post.ContentHtml = rex2.Replace(post.ContentHtml, delegate(Match m)
            {
                string ret = @"<div class='tags'>";
                var tags = m.Groups[1].ToString().Split(',');

                foreach (var tag in tags)
                {
                    ret += @"<span class='label label-primary'>" + tag +  @"</span>";
                }

                ret += @"</div>";
                return ret;
            });

            return View("Index", post);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}