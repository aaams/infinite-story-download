using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace downloadInfiniteStory
{
    class Program
    {
        public static readonly string INFINITE_STORY_URL_BASE = "http://infinite-story.com/";
        public static readonly string ISUrl = "http://infinite-story.com/story/room.php?id=";

        public static String baseFilePath = "D:\\Documents\\chooseYourOwnStory\\";
        public static string baseFileName = "myPdf.pdf";

        //ground zero "36382"
        //eternal "94415"

        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("Failed to parse command line arguments.  Try running help command");
                return;
            }

            baseFilePath = Path.GetDirectoryName(options.OutputPath);
            baseFileName = Path.GetFileName(options.OutputPath);

            var pageMap = new Dictionary<string, ISPage>();
            String baseRoom = options.RoomId; 

            if(options.BuildHtml)
            {
                BuildHtml(pageMap, baseRoom);
            }
            else
            {
                BuildPDF(pageMap, baseRoom);
            }
        }

        private static void BuildPDF(Dictionary<string, ISPage> pageMap, string baseRoom, int test)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, memStream);
                document.Open();
                document.Add(new Paragraph("Hello World"));
                //document.Close();
                //writer.Close();

                //File.WriteAllBytes(Path.Combine(baseFilePath, baseFileName), memStream.GetBuffer());
                using (FileStream file = new FileStream(Path.Combine(baseFilePath, baseFileName), FileMode.Create, FileAccess.Write))
                {
                     memStream.WriteTo(file);
                }

                document.Close();
                writer.Close();
            }
        }

        private static void BuildPDF(Dictionary<string, ISPage> pageMap, string baseRoom)
        {
            Dictionary<string, int> chapterPageMap = new Dictionary<string, int>();
            BuildPDF(pageMap, baseRoom, chapterPageMap, true);
            BuildPDF(pageMap, baseRoom, chapterPageMap, false);
        }

        private static void BuildPDF(Dictionary<string, ISPage> pageMap, string baseRoom, Dictionary<string, int> chapterPageMap, bool fakeRun)
        {
            if (fakeRun)
            {
                BuildAllISPages(pageMap, baseRoom);
            }
            //now that we have all the pages, we'll have to clean them up and decide on pages

            Dictionary<string, int> ISPageToPhysicalPageMap = new Dictionary<string, int>();

            int currentPage = 1;
            int currentChapter = 1;
            Random r = new Random(123456);
            List<string> pagesLeft = new List<string>(pageMap.Count);
            foreach(string x in pageMap.Keys) 
            {
                pagesLeft.Add(x);
            }

            using (MemoryStream memStream = new MemoryStream())
            {
                Document pdfDoc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memStream);
                HeaderFooter evnt = new HeaderFooter();
                if(fakeRun)
                {
                    evnt.pageByTitle = chapterPageMap;
                }
                else
                {
                    evnt.pageByTitle = new Dictionary<string,int>();
                }
                if (!fakeRun)
                {
                    writer.PageEvent = evnt;
                }

                pdfDoc.Open();
                pdfDoc.AddAuthor("test");
                pdfDoc.AddTitle("testTitle");

                while (pagesLeft.Any())
                {

                    string pageToAdd = pagesLeft.First();
                    if(currentPage > 1)
                    {
                        pagesLeft.Skip(r.Next(pagesLeft.Count)).First();
                    }
                    pagesLeft.Remove(pageToAdd);

                    if (fakeRun)
                    {
                        chapterPageMap.Add(pageToAdd, writer.PageNumber + 1);
                    }

                    ISPageToPhysicalPageMap.Add(pageToAdd, currentPage);

                    var chapter = GetPDFPage(pageMap[pageToAdd], int.Parse(pageToAdd), chapterPageMap);
                    pdfDoc.Add(chapter);

                    int actualPageLength = fakeRun ? 1 : chapterPageMap[pageToAdd];

                    currentPage += actualPageLength;
                    currentChapter++;
                }

                pdfDoc.Close();
                writer.Close();

                if(!fakeRun)
                {
                    File.WriteAllBytes(Path.Combine(baseFilePath, baseFileName), memStream.GetBuffer());
                }
            }
        }

        static int chapterCount = 1;
        private static Chapter GetPDFPage(ISPage iSPage, int chapterNum, Dictionary<string, int> map)
        {

            Chapter chap = PdfChapterHelper.HtmlToPdfString(iSPage, chapterNum, map);

            return chap;
        }

        private static void BuildAllISPages(Dictionary<string, ISPage> pageMap, string roomId)
        {
            String baseHtml = ReadHtml(roomId);
            ISPage page = ISPageHtmlParser.ParseRawISHtml(baseHtml);
            pageMap.Add(roomId, page);
            foreach (String childRoomId in page.Choices.Select(x => x.RoomId))
            {
                if (!pageMap.ContainsKey(childRoomId))
                {
                    BuildAllISPages(pageMap, childRoomId);
                }
            }
        }

        private static void BuildHtml(Dictionary<string, ISPage> pageMap, string roomId)
        {
            String baseHtml = ReadHtml(roomId);
            ISPage page = ISPageHtmlParser.ParseRawISHtml(baseHtml);
            pageMap.Add(roomId, page);
            SavePage(roomId, page);
            foreach(String childRoomId in page.Choices.Select(x=>x.RoomId)) 
            {
                if (!pageMap.ContainsKey(childRoomId))
                {
                    BuildHtml(pageMap, childRoomId);
                }
            }
        }

        private static void SavePage(string roomId, ISPage page)
        {
            File.AppendAllText(baseFilePath + roomId + ".html", page.GetHtmlContents());
        }

        private static string ReadHtml(string roomId)
        {
            WebRequest request = WebRequest.Create(ISUrl + roomId);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            return html;
        }
    }
}
