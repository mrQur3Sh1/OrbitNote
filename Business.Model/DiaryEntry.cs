using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookMVVM.Business.Model
{
    [Serializable]
    public class DiaryEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool IsFavorite { get; set; }

        // Optional state logic to add later
        public NoteState State { get; set; } = NoteState.Draft;


    }
}
