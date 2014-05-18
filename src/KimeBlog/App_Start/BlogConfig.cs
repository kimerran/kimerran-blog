using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimeBlog.App_Start
{
    public static class BlogConfig
    {
        public static string PATH_POSTS = System.Web.Hosting.HostingEnvironment.MapPath("/Static/posts/");
        public static string FB_USERNAME = "markhughneri";
        public static string TW_USERNAME = "markhughneri";
    }
}