// In Views/NewSpacePopup.xaml.cs
namespace MauiApp2.Views;

public partial class NewSpacePopup : ContentView
{
    // Define events that the main page can listen for
    public event EventHandler SaveClicked;
    public event EventHandler CancelClicked;
    public event EventHandler ChooseIconClicked;
    
    private string _icon;

    // This property allows MainPage to update the icon
    public string SelectedIcon
    {
        get => _icon;
        set
        {
            _icon = value;
            SelectedIconLabel.Text = _icon; // Update the UI
        }
    }

    public NewSpacePopup()
    {
        InitializeComponent();
    }

    // Public property to easily get the entered name
    public string SpaceName => SpaceNameEntry.Text;
    public string SpaceIcon => SelectedIcon;
    
    public void Reset()
    {
        // Clear the name entry
        SpaceNameEntry.Text = string.Empty;
    
        // Reset the icon to the default
        SelectedIcon = "🫥";
    }
    
    private void OnChooseIconClicked(object sender, EventArgs e)
    {
        ChooseIconClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        // When the save button inside the popup is clicked, fire the public event (broadcast to all of the project)
        if (SaveClicked != null)
        {
            SaveClicked.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        // When the cancel button is clicked, fire the public event
        if (CancelClicked != null)
        {
            CancelClicked.Invoke(this, EventArgs.Empty);
        }
    }
}