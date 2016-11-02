using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyStore.WebUI.Models;
using System.Web.Mvc;
using System.Text;

namespace MyStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {

        public static MvcHtmlString PageLink(this HtmlHelper html, PagingInfo paginginfo, Func<int, string> pageUrl)
        {

            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= paginginfo.TotalPages; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl(i));
                tagBuilder.InnerHtml = i.ToString();
                if (i == paginginfo.CurrentPage)
                {
                    tagBuilder.AddCssClass("selected");

                }
                result.Append("<div class='btn-group'>" + tagBuilder.ToString()+"</div>");

            }

            return MvcHtmlString.Create("<div class='btn-toolbar'>" + result.ToString()+"</div>");

        }
    }
}