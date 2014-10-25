using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace BlogMvc5.Extensions
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string Src)
        {
            string img = string.Format("<img src='{0}'>",Src);
            return MvcHtmlString.Create(img);
        }
    }
}