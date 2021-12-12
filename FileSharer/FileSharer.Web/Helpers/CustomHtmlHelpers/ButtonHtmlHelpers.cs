using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FileSharer.Web.Helpers.CustomHtmlHelpers
{
    public static class ButtonHtmlHelpers
    {
        public static HtmlString EditButton(this IHtmlHelper html, ClaimsPrincipal user, string roles, int routeId)
        {
            var htmlString = new HtmlString(string.Empty);

            var rolesList = roles.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var role in rolesList)
            {
                if (user.IsInRole(role))
                {
                    TagBuilder link = new TagBuilder("a");
                    link.Attributes.Add("class", "btn btn-outline-warning text-dark m-1");
                    link.Attributes.Add("href", $"Edit/{routeId}");

                    link.InnerHtml.AppendHtml("Edit");

                    htmlString = ToHtmlString(link);
                }
            }

            return htmlString;
        }

        public static HtmlString DeleteButton(this IHtmlHelper html, ClaimsPrincipal user, string roles, int routeId)
        {
            var htmlString = new HtmlString(string.Empty);

            var rolesList = roles.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var role in rolesList)
            {
                if (user.IsInRole(role))
                {
                    TagBuilder button = new TagBuilder("button");
                    button.Attributes.Add("class", "btn btn-outline-danger m-1");
                    button.Attributes.Add("data-toggle", "modal");
                    button.Attributes.Add("data-target", $"#modal-{routeId}");

                    button.InnerHtml.Append("Delete");

                    htmlString = ToHtmlString(button);
                }
            }

            return htmlString;
        }

        public static HtmlString DownloadButton(this IHtmlHelper html, int routeId, string name)
        {
            TagBuilder link = new TagBuilder("a");
            link.Attributes.Add("class", "btn btn-outline-primary rounded-circle m-1");
            link.Attributes.Add("href", $"Download/{routeId}?name={name}");

            link.InnerHtml.AppendHtml("&#8659;");

            var htmlString = ToHtmlString(link);

            return htmlString;
        }

        public static HtmlString DetailsButton(this IHtmlHelper html, int routeId)
        {
            TagBuilder link = new TagBuilder("a");
            link.Attributes.Add("class", "btn btn-outline-info m-1");
            link.Attributes.Add("href", $"Details/{routeId}");
            link.InnerHtml.AppendHtml("Details");

            var htmlString = ToHtmlString(link);

            return htmlString;
        }

        public static HtmlString BackToListButton(this IHtmlHelper html, string controller)
        {
            TagBuilder link = new TagBuilder("a");
            link.Attributes.Add("class", "btn btn-outline-primary mx-md-2");
            link.Attributes.Add("href", $"/{controller}/List");
            link.InnerHtml.AppendHtml("Back to list");

            var htmlString = ToHtmlString(link);

            return htmlString;
        }

        private static HtmlString ToHtmlString(TagBuilder tag)
        {
            var htmlWriter = new StringWriter();
            tag.WriteTo(htmlWriter, HtmlEncoder.Default);
            var htmlString = new HtmlString(htmlWriter.ToString());

            return htmlString;
        }
    }
}
