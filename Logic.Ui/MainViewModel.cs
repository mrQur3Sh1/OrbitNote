using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotebookMVVM.Business.Model;
using NotebookMVVM.Services.SerializationService;
using NotebookMVVM.Ui.Desktop;

namespace NotebookMVVM.Logic.Ui
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<DiaryEntryViewModel> Notes { get; set; } = new();
        public ObservableCollection<DiaryEntryViewModel> FilteredNotes { get; set; } = new();

        [ObservableProperty] private string searchQuery;
        [ObservableProperty] private bool showArchived;
        [ObservableProperty] private DiaryEntryViewModel selectedNote;

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SaveBinaryCommand { get; }
        public ICommand LoadBinaryCommand { get; }
        public ICommand SaveXmlCommand { get; }
        public ICommand LoadXmlCommand { get; }
        public ICommand ExportPdfCommand { get; }
        public ICommand ExportHtmlCommand { get; }
        public ICommand ViewStatsCommand { get; }
        public ICommand PrintCommand { get; }

        private readonly RelayCommand editNoteCommand;
        public ICommand EditNoteCommand => editNoteCommand;

        public MainViewModel()
        {
            AddNoteCommand = new RelayCommand(AddNote);
            DeleteNoteCommand = new RelayCommand(DeleteSelectedNote);
            SaveBinaryCommand = new RelayCommand(SaveNotesToBinary);
            LoadBinaryCommand = new RelayCommand(LoadNotesFromBinary);
            SaveXmlCommand = new RelayCommand(SaveNotesToXml);
            LoadXmlCommand = new RelayCommand(LoadNotesFromXml);
            ExportPdfCommand = new RelayCommand(ExportToPdf);
            ExportHtmlCommand = new RelayCommand(ExportToHtml);
            ViewStatsCommand = new RelayCommand(ViewStatistics);
            PrintCommand = new RelayCommand(PrintNotes);
            editNoteCommand = new RelayCommand(EditSelectedNote, () => SelectedNote != null);

            LoadNotesFromBinary();
        }

        partial void OnSearchQueryChanged(string value) => FilterNotes(value);
        partial void OnShowArchivedChanged(bool value) => FilterNotes(SearchQuery);

        partial void OnSelectedNoteChanged(DiaryEntryViewModel value)
        {
            editNoteCommand.NotifyCanExecuteChanged();
        }

        public void FilterNotes(string query)
        {
            FilteredNotes.Clear();
            foreach (var note in Notes)
            {
                if (!ShowArchived && note.State == NoteState.Archived)
                    continue;

                if (string.IsNullOrWhiteSpace(query) ||
                    note.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    note.Content.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    note.State.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredNotes.Add(note);
                }
            }
        }

        private void AddNote()
        {
            var editor = new NoteEditor();

            if (editor.DataContext is NoteEditorViewModel vm)
            {
                vm.LoadForAdding(); // 🟢 This sets IsAddMode = true and updates the bindings
            }

            if (editor.ShowDialog() == true && editor.DataContext is NoteEditorViewModel confirmedVm && confirmedVm.NewNote != null)
            {
                Notes.Add(confirmedVm.NewNote);
                FilterNotes(SearchQuery);
            }
        }


        private void EditSelectedNote()
        {
            if (SelectedNote == null)
                return;

            if (SelectedNote.State == NoteState.Final)
            {
                MessageBox.Show("Final notes cannot be edited.", "Read-Only Note", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editor = new NoteEditor();
            if (editor.DataContext is NoteEditorViewModel editVm)
            {
                editVm.Title = SelectedNote.Title;
                editVm.Content = SelectedNote.Content;
                editVm.IsFavorite = SelectedNote.IsFavorite;
                editVm.CreatedOn = SelectedNote.CreatedOn;
                editVm.State = SelectedNote.State;
            }

            if (editor.ShowDialog() == true)
            {
                if (editor.DataContext is NoteEditorViewModel updatedVm)
                {
                    SelectedNote.Title = updatedVm.Title;
                    SelectedNote.Content = updatedVm.Content;
                    SelectedNote.IsFavorite = updatedVm.IsFavorite;
                    SelectedNote.State = updatedVm.State;
                }
            }
        }

        private void DeleteSelectedNote()
        {
            if (SelectedNote == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the note \"{SelectedNote.Title}\"?",
                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Notes.Remove(SelectedNote);
                FilterNotes(SearchQuery);
            }
        }

        private void SaveNotesToBinary()
        {
            var entries = Notes.Select(n => new DiaryEntry
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedOn = n.CreatedOn,
                IsFavorite = n.IsFavorite,
                State = n.State
            }).ToList();

            BinaryNoteStorage.Save(entries);
        }

        private void LoadNotesFromBinary()
        {
            Notes.Clear();

            var savedEntries = BinaryNoteStorage.Load();
            foreach (var entry in savedEntries)
            {
                Notes.Add(new DiaryEntryViewModel
                {
                    Id = entry.Id,
                    Title = entry.Title,
                    Content = entry.Content,
                    CreatedOn = entry.CreatedOn,
                    IsFavorite = entry.IsFavorite,
                    State = entry.State
                });
            }

            FilterNotes("");
        }

        private void SaveNotesToXml()
        {
            var entries = Notes.Select(n => new DiaryEntry
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedOn = n.CreatedOn,
                IsFavorite = n.IsFavorite,
                State = NoteState.Final
            }).ToList();

            XmlNoteStorage.Save(entries);
            MessageBox.Show("Notes saved to XML.");
        }

        private void LoadNotesFromXml()
        {
            Notes.Clear();

            var loaded = XmlNoteStorage.Load();
            foreach (var entry in loaded)
            {
                Notes.Add(new DiaryEntryViewModel
                {
                    Id = entry.Id,
                    Title = entry.Title,
                    Content = entry.Content,
                    CreatedOn = entry.CreatedOn,
                    IsFavorite = entry.IsFavorite,
                    State = entry.State
                });
            }

            MessageBox.Show("Notes loaded from XML.");
            FilterNotes(SearchQuery);
        }

        private void ExportToHtml()
        {
            var entries = Notes.Select(n => new DiaryEntry
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedOn = n.CreatedOn,
                IsFavorite = n.IsFavorite,
                State = NoteState.Final
            }).ToList();

            HtmlExporter.ExportToHtml(entries);
        }

        private void ExportToPdf()
        {
            var entries = Notes.Select(n => new DiaryEntry
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedOn = n.CreatedOn,
                IsFavorite = n.IsFavorite,
                State = n.State
            }).ToList();

            PdfExporter.ExportToPdf(entries);
        }

        private void ViewStatistics()
        {
            if (Notes.Count == 0)
            {
                MessageBox.Show("No notes available to analyze.", "Notebook Stats", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var statsVm = new StatisticsViewModel(Notes);
            var statsWindow = new StatisticsWindow(statsVm);
            statsWindow.ShowDialog();
        }

        private void PrintNotes()
        {
            if (Notes.Count == 0)
            {
                MessageBox.Show("There are no notes to print.", "Print", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var doc = new FlowDocument
            {
                PagePadding = new Thickness(50),
                FontSize = 14,
                FontFamily = new FontFamily("Segoe UI")
            };

            foreach (var note in Notes)
            {
                var title = new Paragraph(new Run($"📝 {note.Title}")) { FontWeight = FontWeights.Bold, FontSize = 16 };
                var content = new Paragraph(new Run(note.Content)) { Margin = new Thickness(0, 0, 0, 20) };
                doc.Blocks.Add(title);
                doc.Blocks.Add(content);
            }

            var printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Printing Notes");
            }
        }
    }
}
