using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using NotebookMVVM.Business.Model;


namespace NotebookMVVM.Logic.Ui
{
    public partial class DiaryEntryViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string content = string.Empty;

        [ObservableProperty]
        private DateTime createdOn;

        [ObservableProperty]
        private bool isFavorite;

        public NoteState State { get; set; }

    }
}
