using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace downloadInfiniteStory
{
    class Choice
    {
        public string ChoiceId { get; set; }
        public string RoomId { get; set; }
        public string Text { get; set; }

        public Choice(string choiceId, string roomId, string text)
        {
            this.ChoiceId = choiceId;
            this.RoomId = roomId;
            this.Text = text;
        }
    }
}
