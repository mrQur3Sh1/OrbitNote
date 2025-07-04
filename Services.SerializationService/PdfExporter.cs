using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NotebookMVVM.Business.Model;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace NotebookMVVM.Services.SerializationService
{
    public static class PdfExporter
    {
        private static readonly string DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AwpAppData");
        private static readonly string PdfPath = Path.Combine(DirectoryPath, "notes.pdf");

        public static void ExportToPdf(List<DiaryEntry> entries)
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            var doc = new PdfDocument();
            doc.Info.Title = "Exported Notes";

            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Times New Roman", 12, XFontStyle.Regular);


            int y = 40;

            foreach (var note in entries)
            {
                gfx.DrawString($"Title: {note.Title}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Date: {note.CreatedOn}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Content: {note.Content}", font, XBrushes.Black, 40, y); y += 40;

                if (y > page.Height - 100)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;
                }
            }

            doc.Save(PdfPath);
            Process.Start("explorer.exe", PdfPath); // open PDF file
        }
    }
}
