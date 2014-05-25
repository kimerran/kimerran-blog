using KimeBlog.App_Start;
using KimeBlog.Models;
using KimeBlog.ViewModels;
using MarkdownSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                postsContent.Add(KimeBlogApp.GeneratePost(post.Id.ToString(), ""));
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
        //[OutputCache(Duration=20)]
        [HttpGet]
        public ActionResult Read(string id, string url)
        {
            BlogPost post;
            try
            {
                post = KimeBlogApp.GeneratePost(id, url);
            }
            catch (Exception)
            {
                return new RedirectResult("/404");
            }
            
            if (url != post.Url || url == "")  // if wrong url, redirect to SEO-friendly version
            {
                return new RedirectResult(String.Format("/b/{0}/{1}", post.Id, post.Url));
            }




            var blogpost = new BlogpostViewModel {
               Post = post
            };

            if (!string.IsNullOrEmpty(post.Preview))
            {
                blogpost.MetaList.Add("description", post.Preview);
            }
           
            
            if (post.Tags.Contains("archive"))
            {
                blogpost.MetaList.Add("robots", "noindex");   
            }
            return View(blogpost);
        }

     
       
    }
}