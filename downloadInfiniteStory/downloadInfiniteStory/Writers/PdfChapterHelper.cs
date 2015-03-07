using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace downloadInfiniteStory
{
    class PdfChapterHelper
    {
        //private static int chapterCount = 1;

        public static Chapter HtmlToPdfString(ISPage iSPage, int chapterNum, Dictionary<string, int> pageMap)
        {
            String htmlInput = iSPage.Contents;

            Chapter chapter = new Chapter(chapterNum.ToString(), 1);

            Font currentFont = FontFactory.GetFont(FontFactory.HELVETICA);

            SetAnchor(iSPage, chapter, pageMap);
            WriteContents(htmlInput, chapter);

            if (String.IsNullOrWhiteSpace(iSPage.EndText))
            {
                WriteChoices(iSPage, pageMap, chapter); 
            }
            else
            {
                WriteEnd(iSPage, chapter);
            }

            return chapter;
        }

        private static void SetAnchor(ISPage iSPage, Chapter chapter, Dictionary<string, int> pageMap)
        {
            Anchor anchorTarget = new Anchor(iSPage.RoomId);
            anchorTarget.Name = iSPage.RoomId;
            Paragraph targetParagraph = new Paragraph();
            targetParagraph.Add(anchorTarget);
            chapter.Add(targetParagraph);
        }

        private static void WriteContents(String htmlInput, Chapter chapter)
        {
            Font currentFont = FontFactory.GetFont(FontFactory.HELVETICA);

            string output = htmlInput;
            int position = 0;
            while (position < output.Length)
            {
                int nextLT = output.Substring(position).IndexOf('<') + position;
                if (nextLT < 0)
                {
                    break;
                }

                if (position != nextLT)
                {
                    chapter.Add(new iTextSharp.text.Chunk(output.Substring(position, nextLT - position), currentFont));
                }

                int nextGT = output.Substring(position).IndexOf('>') + position;
                string tag = output.Substring(nextLT, nextGT - nextLT);

                position = nextGT + 1;

                if (tag.StartsWith("<img"))  //insert image
                {
                    if (tag.StartsWith("<img src=\""))
                    {
                        int startQuote = tag.IndexOf("\"") + 1;
                        int endQuote = tag.Substring(startQuote).IndexOf("\"") + startQuote;
                        string imgPath = tag.Substring(startQuote, endQuote - startQuote);

                        chapter.Add(Image.GetInstance(imgPath));
                    }
                }
                else if (tag.StartsWith("<p>")) //insert paragraph
                {
                    chapter.Add(new iTextSharp.text.Chunk("\t", currentFont));
                }
                else if (tag.StartsWith("<br>")) //insert line break
                {
                    chapter.Add(new iTextSharp.text.Phrase("\r\n", currentFont));
                }
                else if (tag.StartsWith("<b>")) //insert bold
                {
                    if (currentFont.IsItalic())
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLDOBLIQUE);
                    }
                    else
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                    }
                }
                else if (tag.StartsWith("<i>")) //insert italics
                {
                    if (currentFont.IsBold())
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLDOBLIQUE);
                    }
                    else
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE);
                    }
                }
                else if (tag.StartsWith("</b>")) //insert bold
                {
                    if (currentFont.IsItalic())
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE);
                    }
                    else
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA);
                    }
                }
                else if (tag.StartsWith("</i>")) //insert italics
                {
                    if (currentFont.IsBold())
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                    }
                    else
                    {
                        currentFont = FontFactory.GetFont(FontFactory.HELVETICA);
                    }
                }

            }
        }

        private static void WriteChoices(ISPage iSPage, Dictionary<string, int> pageMap, Chapter chapter)
        {
            chapter.Add(new iTextSharp.text.Phrase(String.Format("\nYou have {0} choices:\n", iSPage.Choices.Count)));
            foreach (Choice choice in iSPage.Choices)
            {
                if(pageMap.ContainsKey(choice.RoomId))
                {
                    Anchor anchor = new Anchor(String.Format("{0} - Page {1}\n", choice.Text, pageMap[choice.RoomId]));
                    anchor.Reference = "#" + choice.RoomId;
                    Paragraph paragraph = new Paragraph();
                    paragraph.Add(anchor);
                    chapter.Add(paragraph);


                    //Chunk chunk = new Chunk(String.Format("{0} - Page {1}\n", choice.Text, pageMap[choice.RoomId]));
                    //PdfAction action = PdfAction.GotoLocalPage(pageMap[choice.RoomId], new PdfDestination(0), );
                    //chunk.SetAction(action);
                }
                else
                {
                    chapter.Add(new iTextSharp.text.Phrase(String.Format("{0} - Page {1}\n", choice.Text, "XXX"))); 
                }
            }
        }

        private static void WriteEnd(ISPage iSPage, Chapter chapter)
        {
            chapter.Add(new iTextSharp.text.Phrase(iSPage.EndText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD)));
        }
    }
}
