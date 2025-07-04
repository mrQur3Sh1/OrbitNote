using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotebookMVVM.Business.Model;

namespace NotebookMVVM.Logic.Ui
{
    public partial class NoteEditorViewModel : ObservableObject
    {
        [ObservableProperty] private string title = string.Empty;
        [ObservableProperty] private string content = string.Empty;
        [ObservableProperty] private bool isFavorite;
        [ObservableProperty] private DateTime createdOn = DateTime.Now;
        [ObservableProperty] private NoteState state = NoteState.Draft;

        public ObservableCollection<NoteState> States { get; }

        public DiaryEntryViewModel? NewNote { get; private set; }

        public ICommand AddNoteCommand { get; }

        public bool IsEditable { get; private set; } = true;
        public bool IsReadOnly => !IsEditable;
        public Visibility IsAddVisible => IsAddMode ? Visibility.Visible : Visibility.Collapsed;
        private bool IsAddMode = false;

        public NoteEditorViewModel()
        {
            States = new ObservableCollection<NoteState>((NoteState[])Enum.GetValues(typeof(NoteState)));
            AddNoteCommand = new RelayCommand(AddNote, CanAddNote);
        }

        public void LoadForViewing(DiaryEntryViewModel note)
        {
            Populate(note);
            IsEditable = false;
            IsAddMode = false;
            RaiseUiBindings();
        }

        public void LoadForEditing(DiaryEntryViewModel note)
        {
            Populate(note);
            IsEditable = true;
            IsAddMode = false;
            RaiseUiBindings();
        }

        public void LoadForAdding()
        {
            Clear();
            IsEditable = true;
            IsAddMode = true;
            RaiseUiBindings();
        }

        private void Populate(DiaryEntryViewModel note)
        {
            Title = note.Title;
            Content = note.Content;
            IsFavorite = note.IsFavorite;
            CreatedOn = note.CreatedOn;
            State = note.State;
        }

        private bool CanAddNote()
        {
            return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Content);
        }

        private void AddNote()
        {
            NewNote = new DiaryEntryViewModel
            {
                Id = Guid.NewGuid().GetHashCode(),
                Title = this.Title,
                Content = this.Content,
                IsFavorite = this.IsFavorite,
                CreatedOn = this.CreatedOn,
                State = this.State
            };




            var window = Application.Current.Windows
    .OfType<Window>()
    .FirstOrDefault(w => w.DataContext == this);

            if (window != null)
            {
                window.DialogResult = true;
            }

        }




        partial void OnTitleChanged(string value)
        {
            (AddNoteCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }

        partial void OnContentChanged(string value)
        {
            (AddNoteCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }




        public void Clear()
        {
            Title = string.Empty;
            Content = string.Empty;
            IsFavorite = false;
            CreatedOn = DateTime.Now;
            State = NoteState.Draft;
        }

        private void RaiseUiBindings()
        {
            OnPropertyChanged(nameof(IsEditable));
            OnPropertyChanged(nameof(IsReadOnly));
            OnPropertyChanged(nameof(IsAddVisible));
        }
    }
}
