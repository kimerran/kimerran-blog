using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KimeBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this._title = textInfo.ToTitleCase(value);
            }
        }

        public string Url { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ContentHtml { get; set; }

        public string Content { get; set; }
        public string Preview { get; set; }

        public string FacebookUsername { get; set; }
        public string TwitterUsername { get; set; }

        public List<String> Tags { get; set; }
        public bool CanPrev { get; set; }
        public bool CanNext { get; set; }

        public int NextId { get; set; }
        public int PrevId { get; set; }

        public string ThumbUrl { get; set; }
    }
}