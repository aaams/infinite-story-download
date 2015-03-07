using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace downloadInfiniteStory
{
    class ISPage
    {
        private string BaseHtml { get; set; }
        public String Contents { get; set; }
        public IList<Choice> Choices { get; set; }
        public String EndText { get; set; } //null for non-end pages
        public int PageNumber { get; set; } //only used for PDF generation
        public string RoomId { get; set; }

        public IDictionary<String, String> imageMap { get; set; }

        public ISPage(string roomId, string baseHtml)
        {
            this.RoomId = roomId;
            this.BaseHtml = baseHtml;
            this.Choices = new List<Choice>();
            this.imageMap = new Dictionary<String, String>();
        }
    }
}
