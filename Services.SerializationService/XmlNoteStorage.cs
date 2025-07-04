using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NotebookMVVM.Business.Model;

namespace NotebookMVVM.Services.SerializationService
{
    public static class XmlNoteStorage
    {
        private static readonly string SolutionRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        private static readonly string DirectoryPath = Path.Combine(SolutionRoot, "AwpAppData");
        private static readonly string XmlFilePath = Path.Combine(DirectoryPath, "notes.xml");

        public static void Save(List<DiaryEntry> entries)
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            var serializer = new XmlSerializer(typeof(List<DiaryEntry>));
            using (var writer = new StreamWriter(XmlFilePath))
            {
                serializer.Serialize(writer, entries);
            }
        }

        public static List<DiaryEntry> Load()
        {
            if (!File.Exists(XmlFilePath))
                return new List<DiaryEntry>();

            var serializer = new XmlSerializer(typeof(List<DiaryEntry>));
            using (var reader = new StreamReader(XmlFilePath))
            {
                return (List<DiaryEntry>)serializer.Deserialize(reader);
            }
        }
    }
}
