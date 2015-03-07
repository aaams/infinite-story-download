using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace downloadInfiniteStory
{
    class HeaderFooter : PdfPageEventHelper
    {
        public Dictionary<string, int> pageByTitle { get; set; }


        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, Document document)
        {
            PdfPTable table = new PdfPTable(3);
            try
            {
                table.SetWidths(new int[] { 24, 24, 2 });
                table.TotalWidth = 527;
                table.LockedWidth = true;
                table.DefaultCell.FixedHeight = 20;
                table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(string.Format("{0}", writer.PageNumber));
                table.WriteSelectedRows(0, -1, 34, 803, writer.DirectContent);
            }
            catch (DocumentException de)
            {
                throw de;
            }
        }

        //public override void OnEndPage(PdfWriter writer, Document document)
        //{
        //    //base.OnEndPage(writer, document);
        //    //Rectangle page = document.PageSize;
        //    //ColumnText.ShowTextAligned(
        //    //  writer.DirectContent, Element.ALIGN_CENTER,
        //    //  new Phrase(string.Format("{1}", document.PageNumber)),
        //    //    /* x write coordinate => center */
        //    //  (page.Left + page.Right) / 2,
        //    //    /* y write coordinate => top */
        //    //  page.Height - document.TopMargin,
        //    //  0
        //    //);


        //    base.OnEndPage(writer, document);

        //    int pageN = writer.PageNumber;
        //    String text = writer.PageNumber.ToString();
        //    float len = this.RunDateFont.BaseFont.GetWidthPoint(text, this.RunDateFont.Size);

        //    iTextSharp.text.Rectangle pageSize = document.PageSize;

        //    cb.SetRGBColorFill(100, 100, 100);

        //    cb.BeginText();
        //    cb.SetFontAndSize(this.RunDateFont.BaseFont, this.RunDateFont.Size);
        //    cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
        //    cb.ShowText(text);

        //    cb.EndText();

        //    cb.AddTemplate(template, document.LeftMargin + len, pageSize.GetBottom(document.BottomMargin));
        //}

        //public void onChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
        //{
        //    this.pageByTitle.Add(title.Content, writer.PageNumber);
        //}
    }
}
