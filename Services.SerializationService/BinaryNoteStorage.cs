using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NotebookMVVM.Business.Model;

namespace NotebookMVVM.Services.SerializationService
{
    public static class BinaryNoteStorage
    {
        private static readonly string FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\AwpAppData");
        private static readonly string FilePath = Path.Combine(FolderPath, "notes.dat");

        public static void Save(List<DiaryEntry> entries)
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

#pragma warning disable SYSLIB0011
            using FileStream fs = new FileStream(FilePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, entries);
#pragma warning restore SYSLIB0011
        }

        public static List<DiaryEntry> Load()
        {
            if (!File.Exists(FilePath)) return new List<DiaryEntry>();

#pragma warning disable SYSLIB0011
            using FileStream fs = new FileStream(FilePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            return (List<DiaryEntry>)formatter.Deserialize(fs);
#pragma warning restore SYSLIB0011
        }
    }
}
