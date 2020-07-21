using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Events
{
    public class FooterPdfEventHandler : IEventHandler
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            new Canvas(canvas, new Rectangle(36, 20, page.GetPageSize().GetWidth() - 72, 30)).Add(GetTable(docEvent));


        }

        public Table GetTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 92f, 8f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();
            PdfPage page = docEvent.GetPage();
            int pageNum = docEvent.GetDocument().GetPageNumber(page);
            Style styleCell = new Style().SetPadding(5).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 2));

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToShortDateString()));
            tableEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.RIGHT).SetFontColor(ColorConstants.GRAY));
            cell = new Cell().Add(new Paragraph(pageNum.ToString()));
            cell.AddStyle(styleCell).SetBackgroundColor(ColorConstants.BLACK).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER);
            tableEvent.AddCell(cell);

            return tableEvent;
        }
    }
}
