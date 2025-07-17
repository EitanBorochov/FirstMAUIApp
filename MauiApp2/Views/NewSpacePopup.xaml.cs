// In Views/NewSpacePopup.xaml.cs

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp2.Views;

public partial class NewSpacePopup : ContentView
{
    // Define events that the main page can listen for
    public event EventHandler SaveClicked;
    public event EventHandler CancelClicked;
    public event EventHandler ChooseIconClicked;
    public event EventHandler ChooseColorClicked;
    
    private string _icon;
    // This property allows HomePage to update the icon
    public string SelectedIcon
    {
        get => _icon;
        set
        {
            _icon = value;
            SelectedIconLabel.Text = _icon; // Update the UI
        }
    }
    
    private Color _selectedColor;
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            OnPropertyChanged(); // This notifies the UI that the color has changed
        }
    }

    public NewSpacePopup()
    {
        InitializeComponent();

        this.BindingContext = this;
        Reset();
    }

    // Public property to easily get the entered name
    public string SpaceName => SpaceNameEntry.Text;
    public string SpaceIcon => SelectedIcon;
    
    public void Reset()
    {
        // Clear the name entry
        SpaceNameEntry.Text = string.Empty;
    
        // Reset the icon and color to the default
        SelectedIcon = "🫥";
        // Try to find the color with the key "Primary" in your app's resources
        if (Application.Current.Resources.TryGetValue("PrimaryDark", out var primaryColor))
        {
            // If it's found, set the SelectedColor to it
            SelectedColor = (Color)primaryColor;
        }
        else
        {
            // As a fallback, use a default color if "Primary" isn't found
            SelectedColor = Colors.Gray;
        }
    }
    
    private void OnChooseIconClicked(object sender, EventArgs e)
    {
        ChooseIconClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnChooseColorClicked(object sender, EventArgs e)
    {
        ChooseColorClicked?.Invoke(this, EventArgs.Empty); 
    }
    
    private void OnSaveClicked(object sender, EventArgs e)
    {
        // When the save button inside the popup is clicked, fire the public event (broadcast to all of the project)
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        // When the cancel button is clicked, fire the public event
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
    
    // --- INotifyPropertyChanged Implementation ---
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}