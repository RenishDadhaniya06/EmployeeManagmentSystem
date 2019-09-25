
namespace EmployeeManagementSystem.Models
{
    #region Using
    using EmployeeManagementSystem.Helper;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Repository.Classes;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using RazorEngine;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    #endregion

    /// <summary>
    /// PdfBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PdfBuilder<T>
    {
        #region Properties
        private readonly T _post;

        private readonly string _file;

        private readonly SettingView setting;

        private readonly RepositoryContext _repositoryDbContext = new RepositoryContext();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBuilder{T}"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="file">The file.</param>
        public PdfBuilder(T post, string file)
        {
            _post = post;
            _file = file;
            setting = _repositoryDbContext.SettingView.SingleOrDefault();
        }
        /// <summary>
        /// Gets the PDF.
        /// </summary>
        /// <returns></returns>
        ////https://www.danylkoweb.com/Blog/creating-dynamic-pdfs-in-aspnet-mvc-using-itextsharp-EV
        public FileContentResult GetPdf()
        {
            var html = GetHtml();
            Byte[] bytes;
            using (var ms = new MemoryStream())
            {
                //using (var doc = new Document(PageSize.A2))
                using (var doc = new Document(PageSize.A4, 10f, 10f, 100f, 10f))
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        writer.PageEvent = new ITextEvents(setting);
                        doc.Open();
                        try
                        {

                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance()
                                    .ParseXHtml(writer, doc, msHtml, System.Text.Encoding.UTF8);
                            }
                        }
                        finally
                        {
                            doc.Close();
                        }
                    }
                }
                bytes = ms.ToArray();
            }
            return new FileContentResult(bytes, "application/pdf");
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <returns></returns>
        private string GetHtml()
        {
            var html = File.ReadAllText(_file);
            return Razor.Parse(html, _post);
        }
    }
}