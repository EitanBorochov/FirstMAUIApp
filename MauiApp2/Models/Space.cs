// In Models/Space.cs

using System.Collections.ObjectModel; // We will use this later

namespace MauiApp2.Models
{
    public class Space
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        // We will add lists for notes and to-do items here later
        // public ObservableCollection<string> Notes { get; set; }
        // public ObservableCollection<string> TodoItems { get; set; }
    }
}