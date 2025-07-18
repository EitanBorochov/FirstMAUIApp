// In Services/SpaceService.cs

using System.Collections.ObjectModel;
using MauiApp2.Models;

namespace MauiApp2.Managers
{
    public class SpaceManager
    {
        // Creating the one and only instance of the space manager
        public static SpaceManager Main { get; } = new SpaceManager();
        
        // A list that others can view but only this class can modify. Updates UI elements automatically
        public ObservableCollection<Space> Spaces { get; private set; }

        // Constructor
        private SpaceManager()
        {
            // Initialize the list
            Spaces = new ObservableCollection<Space>();
        }

        // Method to add a new space
        public void AddSpace(string name, string icon, Color color)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(icon))
            {
                Space newSpace = new Space { Name = name, Icon = icon, Color = color};
                Spaces.Add(newSpace);
            }
        }
    }
}