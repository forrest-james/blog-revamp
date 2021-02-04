using Markdig;
using Markdig.Renderers;
using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.RegularExpressions;

namespace UI.Services
{
    public class MarkdownService
    {
        public static MarkdownPipeline Pipeline;
        public static string TagBlackList = "script|iframe|object|embed|form";

        public MarkdownService() => Pipeline = CreatePipelineBuilder().Build();

        public static HtmlString ParseHtml(string markdown) => new HtmlString(Parse(markdown));

        public static string SanitizeHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;
            return Regex.Replace(html, $@"(<({TagBlackList})\b[^<]*(?:(?!<\/({TagBlackList}))<[^<[*)*<\/({TagBlackList})>)",
                "",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static string Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                return string.Empty;

            string html;
            using (var htmlWriter = new StringWriter())
            {
                var renderer = CreateRenderer(htmlWriter);
                Markdown.Convert(markdown, renderer, Pipeline);
                html = SanitizeHtml(htmlWriter.ToString());
            }

            return html;
        }

        public virtual MarkdownPipelineBuilder CreatePipelineBuilder()
        {
            MarkdownPipelineBuilder builder = new MarkdownPipelineBuilder();
            return builder
                .UseEmphasisExtras()
                .UsePipeTables()
                .UseGridTables()
                .UseFooters()
                .UseFootnotes()
                .UseCitations()
                .UseAutoLinks()
                .UseAbbreviations()
                .UseMediaLinks()
                .UseListExtras()
                .UseTaskLists()
                .UseGenericAttributes();
        }

        protected static IMarkdownRenderer CreateRenderer(TextWriter writer) => new HtmlRenderer(writer);
    }
}