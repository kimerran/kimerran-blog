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
    [RoutePrefix("b")]
    public class BlogController : Controller
    {


        [Route("search")]
        [HttpGet]
        public ActionResult Search(string q)
        {
            string alljson = System.IO.File.ReadAllText(BlogConfig.PATH_POSTS + "all.json");

            List<BlogPost> posts = JsonConvert.DeserializeObject<List<BlogPost>>(alljson);

            List<BlogPost> postsContent = new List<BlogPost>();
            List<BlogPost> searchResults = new List<BlogPost>();
            
            foreach (var post in posts)
            {
                postsContent.Add(GeneratePost(post.Id.ToString(), ""));
            }

            // check for any matches
            searchResults = postsContent.Where(o => o.ContentHtml.Contains(q)).ToList();

            return View(searchResults);
        }


        [Route("{id}")]
        [HttpGet]
        public ActionResult Read(string id)
        {
            return this.Read(id, "");
        }

        [Route("{id}/{url}")]
        [OutputCache(Duration=20)]
        [HttpGet]
        public ActionResult Read(string id, string url)
        {
            BlogPost post = GeneratePost(id, url);

            if (url != post.Url || url == "")  // if wrong url, redirect to SEO-friendly version
            {
                return new RedirectResult(String.Format("/b/{0}/{1}", post.Id, post.Url));
            }

            return View(post);
        }



        private BlogPost GeneratePost(string id, string url = "")
        {
            
            Match match = Regex.Match(System.IO.File.ReadAllText(
                            BlogConfig.PATH_POSTS + String.Format(@"{0}.md", id)),
                            @"%(\{[^\}]+\})%([^*]+)",
                            RegexOptions.IgnoreCase);

            BlogPost post = JsonConvert.DeserializeObject<BlogPost>(match.Groups[1].ToString());
            
         
            

            post.FacebookUsername = BlogConfig.FB_USERNAME;
            post.TwitterUsername = BlogConfig.TW_USERNAME;

            string contentHtml =  (new Markdown()).Transform(match.Groups[2].ToString());

            // dirty conversion of <img> to responsive
            contentHtml = contentHtml.Replace(@"<img", @"<img class='img img-responsive ' ");
            post.ContentHtml = contentHtml;
            post.CanNext = false;
            post.CanPrev = false;

            int Id;
            Int32.TryParse(id, out Id);
 
            // check if there's prev and next
            if (System.IO.File.Exists(BlogConfig.PATH_POSTS + String.Format(@"{0}.md", Id - 1)))
            {
                post.CanPrev = true;
                post.PrevId = Id - 1;
            }
            if (System.IO.File.Exists(BlogConfig.PATH_POSTS + String.Format(@"{0}.md", Id + 1)))
            {
                post.CanNext = true;
                post.NextId = Id + 1;
            }
          
            return post;

        }
    }
}