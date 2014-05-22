using KimeBlog.App_Start;
using KimeBlog.Models;
using MarkdownSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KimeBlog
{
    public class KimeBlogApp
    {

        private static BlogPost _ExtractContent(string id)
        {
            Match match = Regex.Match(System.IO.File.ReadAllText(
                           BlogConfig.PATH_POSTS + String.Format(@"{0}.md", id)),
                           @"%(\{[^\}]+\})%([^*]+)",
                           RegexOptions.IgnoreCase);

            BlogPost post = JsonConvert.DeserializeObject<BlogPost>(match.Groups[1].ToString());

            post.FacebookUsername = BlogConfig.FB_USERNAME;
            post.TwitterUsername = BlogConfig.TW_USERNAME;

            string mdFileContent = match.Groups[2].ToString();

            //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            //post.Content = textInfo.ToTitleCase(mdFileContent);

            string contentHtml = (new Markdown()).Transform(mdFileContent);

            // dirty conversion of <img> to responsive
            contentHtml = contentHtml.Replace(@"<img", @"<img class='img img-responsive ' ");
            // replace empty <p> tags
            contentHtml = contentHtml.Replace(@"<p></p>", @"");

            var reg2 = new Regex(@"<p>(<.*>.*<.*>)<\/p>", RegexOptions.Multiline);

            contentHtml = reg2.Replace(contentHtml, delegate(Match m)
            {
                return m.Groups[1].ToString();
            });

            contentHtml = contentHtml.Replace("<p><ul>", "<ul>");
            contentHtml = contentHtml.Replace("</ul></p>", "</ul>");

            post.ContentHtml = contentHtml;

            return post;
        }

        public static BlogPost GeneratePost(string id, string url = "")
        {
            BlogPost post = KimeBlogApp._ExtractContent(id);
           
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


        private BlogPost GeneratePage(string id)
        {
            return KimeBlogApp._ExtractContent(id);
        }

    }
}