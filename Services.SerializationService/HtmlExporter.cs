using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NotebookMVVM.Business.Model;

namespace NotebookMVVM.Services.SerializationService
{
    public static class HtmlExporter
    {
        private static readonly string SolutionRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        private static readonly string HtmlPath = Path.Combine(SolutionRoot, "AwpAppData", "notes.html");

        public static void ExportToHtml(List<DiaryEntry> entries)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial; padding: 20px; }");
            sb.AppendLine("h2 { color: #2e6c80; }");
            sb.AppendLine("table { border-collapse: collapse; width: 100%; }");
            sb.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }");
            sb.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Notebook Notes</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><th>Title</th><th>Content</th><th>Date</th><th>Favorite</th><th>State</th></tr>");

            foreach (var note in entries)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{note.Title}</td>");
                sb.AppendLine($"<td>{note.Content}</td>");
                sb.AppendLine($"<td>{note.CreatedOn:dd MMM yyyy}</td>");
                sb.AppendLine($"<td>{(note.IsFavorite ? "Yes" : "No")}</td>");
                sb.AppendLine($"<td>{note.State}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            File.WriteAllText(HtmlPath, sb.ToString());

            System.Windows.MessageBox.Show("Exported to HTML in AwpAppData folder.", "Export Complete");
        }
    }
}
