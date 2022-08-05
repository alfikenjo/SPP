using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Frontend_SPP.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Font = iTextSharp.text.Font;

namespace Frontend_SPP.Common
{
    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;
        BaseFont baseFont = FontFactory.GetFont(FontFactory.HELVETICA).BaseFont;
        float fontsize = 8;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            String text = "Page " + pageN.ToString() + " of ";
            float len = baseFont.GetWidthPoint(text, fontsize);

            iTextSharp.text.Rectangle pageSize = document.PageSize;

            cb.SetRGBColorFill(100, 100, 100);

            cb.BeginText();
            
            cb.SetFontAndSize(baseFont, fontsize);
            cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
            cb.ShowText(text);

            cb.EndText();

            cb.AddTemplate(template, document.LeftMargin + len, pageSize.GetBottom(document.BottomMargin));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);         

            template.BeginText();
            template.SetFontAndSize(baseFont, fontsize);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber));
            template.EndText();
        }
    }
}
