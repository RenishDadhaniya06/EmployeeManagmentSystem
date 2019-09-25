
namespace EmployeeManagementSystem.Helper
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    #endregion


    /// <summary>
    /// ITextEvents
    /// </summary>
    /// <seealso cref="iTextSharp.text.pdf.PdfPageEventHelper" />
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;

        private SettingView setting;

        public ITextEvents(SettingView setting)
        {
            this.setting = setting;
        }
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion



        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        /// Called when the document is opened.
        /// @param writer the <CODE>PdfWriter</CODE> for this document
        /// @param document the document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                cb.MoveTo(40, document.PageSize.Height - 110);
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        /// Called when a page is finished, just before being written to the document.
        /// @param writer the <CODE>PdfWriter</CODE> for this document
        /// @param document the document
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            var FontColour = new BaseColor(216, 84, 89);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, FontColour);
            Phrase p1Header = new Phrase(this.setting.Header, baseFontBig);
            Phrase p2Header = new Phrase(this.setting.SubHeader, baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            //Uri uri = new Uri();
            //Image ima;
            //ima.Url = uri;
            String path = System.Web.HttpContext.Current.Server.MapPath("~/Images/" + this.setting.Logo).ToString();
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            image.SetAbsolutePosition(0, 20);


            image.ScaleAbsolute(60, 50);
            image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
            PdfContentByte cbhead = writer.DirectContent;
            PdfTemplate tp = cbhead.CreateTemplate(273, 95);
            tp.AddImage(image);


            cbhead.AddTemplate(tp, 60, 842 - 95);
            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell(p1Header);
            PdfPCell pdfCell3 = new PdfPCell();

            String text = "Page " + writer.PageNumber + " of ";


            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));
            }

            //Row 2
            PdfPCell pdfCell4 = new PdfPCell(p2Header);
            PdfPCell pdfCell7 = new PdfPCell();
            //Row 3 
            if (this.setting.ShowDate) {
                pdfCell7 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
            }
            PdfPCell pdfCell6 = new PdfPCell();
            PdfPCell pdfCell5 = new PdfPCell();
            //PdfPCell pdfCell7 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), DateTime.Now), baseFontBig));

            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;

            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCell4.Colspan = 3;

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    
            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 90);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 90);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();

        }

        /// <summary>
        /// Called when [close document].
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="document">The document.</param>
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber == 1 ? 1 : writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber == 1 ? 1 : writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}