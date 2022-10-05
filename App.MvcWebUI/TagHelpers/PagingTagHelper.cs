﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace App.MvcWebUI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper:TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("desc")]
        public int Desc { get; set; }
        [HtmlAttributeName("param")]
        public string Param { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var sb = new StringBuilder();
            sb.Append("<ul class='pagination' >");
            for (int i = 1; i <=PageCount; i++)
            {
                sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                sb.AppendFormat("<a class='page-link' href='/product/index?page={0}&category={1}&desc={2}&param={3}'>{4}</a>", i, CurrentCategory, Desc, Param, i);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
