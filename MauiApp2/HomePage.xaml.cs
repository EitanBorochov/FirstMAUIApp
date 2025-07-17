using MauiApp2.Managers;
using MauiApp2.Models;  
using MauiApp2.Views;
using Microsoft.Maui.Dispatching;

namespace MauiApp2;

public partial class HomePage : ContentPage
{
    private bool isCreateMenuOpen = false;
    private IDispatcherTimer greetingsTimer;

    public HomePage()
    {
        InitializeComponent();
        
        // This binds the data from the spaces manager to this page
        this.BindingContext = SpaceManager.Main;

        // Tell the HomePage to listen for the popup's events
        NewSpacePopup.SaveClicked += OnPopupSaveClicked;
        NewSpacePopup.CancelClicked += OnPopupCancelClicked;
        NewSpacePopup.ChooseIconClicked += OnPopupChooseIconClicked;
        NewSpacePopup.ChooseColorClicked += OnPopupChooseColorClicked;
        IconPicker.IconSelected += OnIconPickerIconSelected;
        ColorPicker.ColorSelected += OnColorPickerColorSelected;

        UpdateGreeting();
    }

    private void UpdateGreeting()
    {
        int currentHour = DateTime.Now.Hour;

        if (currentHour >= 5 && currentHour < 12) // 5:00 AM - 11:59 AM
        {
            Title = "☀️ Good Morning, Eitan";
        }
        else if (currentHour >= 12 && currentHour < 18) // 12:00 PM - 5:59 PM
        {
            Title = "🌤️ Good Afternoon, Eitan";
        }
        else if (currentHour >= 18 && currentHour < 22) // 6:00 PM - 9:59 PM
        {
            Title = "🌙 Good Evening, Eitan";
        }
        else // 10:00 PM - 4:59 AM
        {
            Title = "😴 Good Night, Eitan";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        StartGreetingsTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        greetingsTimer?.Stop();
    }

    private void StartGreetingsTimer()
    {
        UpdateGreeting();

        greetingsTimer = Dispatcher.CreateTimer();
        greetingsTimer.Interval = TimeSpan.FromMinutes(1);
        greetingsTimer.Tick += (sender, e) => UpdateGreeting();
        greetingsTimer.Start();
    }

    #region Create button

    private void OnCreateClick(object sender, EventArgs e)
    {
        if (!isCreateMenuOpen)
        {
            OpenCreateMenu();
        }
        else
        {
            CloseCreateMenu();
        }
    }

    private async void OpenCreateMenu()
    {
        isCreateMenuOpen = true;
        
        CreateOptionsStack.IsVisible = true;

        await Task.WhenAll(
            CreateButton.RotateTo(45, 250, Easing.CubicOut),
            CreateOptionsStack.FadeTo(1, 250, Easing.CubicOut),
            CreateOptionsStack.ScaleTo(1, 250, Easing.CubicOut)
        );
    }

    private async void CloseCreateMenu()
    {
        isCreateMenuOpen = false;

        // Run all animations at the same time, then hide the overlays
        await Task.WhenAll(
            CreateButton.RotateTo(0, 250, Easing.CubicIn),
            CreateOptionsStack.FadeTo(0, 250, Easing.CubicIn),
            CreateOptionsStack.ScaleTo(0, 250, Easing.CubicIn)
        );

        CreateOptionsStack.IsVisible = false;
    }

    #endregion

    #region Create space

    private void OnNewSpaceClicked(object? sender, EventArgs e)
    {
        CloseCreateMenu();

        NewSpacePopup.Reset();

        NewSpacePopup.IsVisible = true;
    }
    
    private async void OnPopupSaveClicked(object sender, EventArgs e)
    {
        string newName = NewSpacePopup.SpaceName?.Trim();

        if (string.IsNullOrWhiteSpace(newName))
        {
            await DisplayAlert("Name Required", "Please enter a name for your space.", "OK");
            return;
        }

        if (SpaceManager.Main.Spaces.Any(space => space.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
        {
            await DisplayAlert("Space Already Exists",
                "A space with that name already exists. Please choose a different name", "OK.");
            return;
        }
        
        // This is where the action happens. We get the data from the popup's properties.
        SpaceManager.Main.AddSpace(newName, NewSpacePopup.SpaceIcon, Colors.White);

        // After saving, we hide the popup.
        NewSpacePopup.IsVisible = false;
    }

    private void OnPopupCancelClicked(object sender, EventArgs e)
    {
        // Just hide the popup
        NewSpacePopup.IsVisible = false;
    }
    
    private void OnPopupChooseIconClicked(object sender, EventArgs e)
    {
        NewSpacePopup.IsVisible = false;
        
        // Show the icon picker popup
        IconPicker.IsVisible = true;
    }

    // When an icon is selected in the second popup...
    private void OnIconPickerIconSelected(object sender, string selectedIcon)
    {
        // Pass the selected icon back to the first popup
        NewSpacePopup.SelectedIcon = selectedIcon;
        // Hide the icon picker
        IconPicker.IsVisible = false;

        NewSpacePopup.IsVisible = true;
    }

    private void OnPopupChooseColorClicked(object sender, EventArgs e)
    {
        NewSpacePopup.IsVisible = false;
        
        ColorPicker.IsVisible = true;
    }

    private void OnColorPickerColorSelected(object sender, Color selectedColor)
    {
        NewSpacePopup.SelectedColor = selectedColor;

        ColorPicker.IsVisible = false;

        NewSpacePopup.IsVisible = true;
    }


    #endregion
}