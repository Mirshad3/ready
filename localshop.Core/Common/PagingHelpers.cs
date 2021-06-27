using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace localshop.Core.Common
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            TagBuilder ul = new TagBuilder("ul");

            // Build prev icon
            TagBuilder liPrev = new TagBuilder("li");
            TagBuilder aPrev = new TagBuilder("a");
            aPrev.AddCssClass("prev");
            if (pagingInfo.CurrentPage == 1)
            {
                aPrev.AddCssClass("disabled");
                aPrev.MergeAttribute("href", pageUrl(1));
            }
            else
            {
                aPrev.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
            }

            TagBuilder spanIconPrev = new TagBuilder("span");
            spanIconPrev.AddCssClass("la la-angle-left");
            aPrev.InnerHtml = spanIconPrev.ToString();

            liPrev.InnerHtml = aPrev.ToString();
            ul.InnerHtml += liPrev.ToString();

            // Build list number page
            var startPage = 1;
            var endPage = pagingInfo.TotalPages;
            if (pagingInfo.TotalPages > 7)
            {
                if (pagingInfo.CurrentPage - 3 > 1)
                {
                    startPage = pagingInfo.CurrentPage - 3;
                }
                if (pagingInfo.CurrentPage + 3 < pagingInfo.TotalPages)
                {
                    endPage = pagingInfo.CurrentPage + 3;
                }
                if (startPage == 1 && pagingInfo.TotalPages >= 7)
                {
                    endPage = 7;
                }
                if (endPage == pagingInfo.TotalPages && pagingInfo.TotalPages >= 7)
                {
                    startPage = pagingInfo.TotalPages - 6;
                }
            }


            if (startPage != 1)
            {
                TagBuilder liDot = new TagBuilder("li");
                TagBuilder aDot = new TagBuilder("a");
                aDot.MergeAttribute("href", "javascript:void(0)");
                aDot.AddCssClass("disabled");
                aDot.InnerHtml = "...";
                liDot.InnerHtml = aDot.ToString();
                ul.InnerHtml += liDot.ToString();
            }
            for (int i = startPage; i <= endPage; i++)
            {
                TagBuilder liNum = new TagBuilder("li");
                TagBuilder aNum = new TagBuilder("a");
                aNum.MergeAttribute("href", pageUrl(i));
                aNum.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    aNum.AddCssClass("active");
                }

                liNum.InnerHtml = aNum.ToString();
                ul.InnerHtml += liNum.ToString();
            }
            if (endPage != pagingInfo.TotalPages)
            {
                TagBuilder liDot = new TagBuilder("li");
                TagBuilder aDot = new TagBuilder("a");
                aDot.MergeAttribute("href", "javascript:void(0)");
                aDot.AddCssClass("disabled");
                aDot.InnerHtml = "...";
                liDot.InnerHtml = aDot.ToString();
                ul.InnerHtml += liDot.ToString();
            }

            // Build next icon
            TagBuilder liNext = new TagBuilder("li");
            TagBuilder aNext = new TagBuilder("a");
            aNext.AddCssClass("next");
            if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
            {
                aNext.AddCssClass("disabled");
                aNext.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
            }
            else
            {
                aNext.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
            }

            TagBuilder spanIconNext = new TagBuilder("span");
            spanIconNext.AddCssClass("la la-angle-right");
            aNext.InnerHtml = spanIconNext.ToString();

            liNext.InnerHtml = aNext.ToString();
            ul.InnerHtml += liNext.ToString();

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}
