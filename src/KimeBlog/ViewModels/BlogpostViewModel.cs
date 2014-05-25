using KimeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimeBlog.ViewModels
{
    public class BlogpostViewModel
    {
        public BlogPost Post { get; set; }
        public Dictionary<string,string> MetaList { get; set; }

        public BlogpostViewModel()
        {
            MetaList = new Dictionary<string, string>();
        }
    }
}