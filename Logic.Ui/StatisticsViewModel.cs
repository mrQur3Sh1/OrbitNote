using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace NotebookMVVM.Logic.Ui
{
    public partial class StatisticsViewModel : ObservableObject
    {
        [ObservableProperty] private int totalNotes;
        [ObservableProperty] private int favoriteNotes;
        [ObservableProperty] private double avgWordCount;

        public ICommand CloseCommand { get; }

        public StatisticsViewModel(IEnumerable<DiaryEntryViewModel> notes)
        {
            var list = notes?.ToList() ?? new List<DiaryEntryViewModel>();

            TotalNotes = list.Count;
            FavoriteNotes = list.Count(n => n.IsFavorite);
            AvgWordCount = list.Count > 0
                ? list.Average(n => WordCount(n.Content))
                : 0;

            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Window statWindow && statWindow.Title == "Notebook Stats")
                {
                    statWindow.Close();
                    break;
                }
            }
        }

        private int WordCount(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return 0;
            return content.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
