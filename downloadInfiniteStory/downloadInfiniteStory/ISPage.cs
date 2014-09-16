using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace downloadInfiniteStory
{
    class ISPage
    {
        private static readonly string CHOICES_START_XML = "<div id=\"room-choices\"><h2>You have X choices:</h2><ul class=\"choices\">";
        private static readonly string CHOICES_LINE_XML = "<li><a href=\"PAGEID.html\" title=\"TITLEINFO\">CHOICETEXT</a></li>";
        private static readonly string CHOICES_END_XML = "</ul></div> </div></div></div>";

        private string BaseHtml { get; set; }
        public String Contents { get; set; }
        public IList<Choice> Choices { get; set; }
        public String EndText { get; set; } //null for non-end pages
        public int PageNumber { get; set; } //only used for PDF generation


        public IDictionary<String, String> imageMap { get; set; }

        public ISPage(string baseHtml)
        {
            this.BaseHtml = baseHtml;
            this.Choices = new List<Choice>();
            this.imageMap = new Dictionary<String, String>();
        }

        internal string GetHtmlContents()
        {
            StringBuilder html = new StringBuilder(Contents);

            if (EndText != null)
            {
                html.AppendLine(EndText);
            }
            else
            {
                html.AppendLine(CHOICES_START_XML.Replace("X", Choices.Count.ToString()));
                foreach (Choice choice in Choices)
                {
                    html.AppendLine(CHOICES_LINE_XML.Replace("PAGEID", choice.RoomId).Replace("TITLEINFO", choice.ChoiceId + "," + choice.RoomId).Replace("CHOICETEXT", choice.Text));
                }
                html.AppendLine(CHOICES_END_XML);
            }
            return html.ToString();
        }
    }
}
