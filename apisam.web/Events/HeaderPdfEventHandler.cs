using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
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
    public class HeaderPdfEventHandler : IEventHandler
    {
        //readonly Image Img;
        public HeaderPdfEventHandler()
        {
            //Img = img;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();


            PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetWidth() - 70, 60);
            new Canvas(canvas,rootArea).Add(GetTable(docEvent));



        }

        public Table GetTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();
            PdfPage page = docEvent.GetPage();

            Style styleCell = new Style().SetBorder(Border.NO_BORDER);
            Style styleText = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);
            //Cell cell = new Cell().Add(Img.SetAutoScale(true)).SetBorder(Border.NO_BORDER);
            Cell cell = new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER);
            tableEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.LEFT));
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            cell = new Cell().Add(new Paragraph("Sistema de Administración Medica").SetFont(bold))
                .Add(new Paragraph($"Fecha: {DateTime.Now.ToShortDateString()}").SetFont(bold))
                .AddStyle(styleText).AddStyle(styleCell).SetBorder(Border.NO_BORDER);
            tableEvent.AddCell(cell);
            return tableEvent;
        }
    }
}
