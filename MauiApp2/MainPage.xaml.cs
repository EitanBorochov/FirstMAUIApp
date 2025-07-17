using MauiApp2.Managers;
using MauiApp2.Models;  
using MauiApp2.Views;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    int count = 0;

    private bool isCreateMenuOpen = false;

    public MainPage()
    {
        InitializeComponent();
        this.BindingContext = SpaceManager.Current;

        // Tell the MainPage to listen for the popup's events
        NewSpacePopup.SaveClicked += OnPopupSaveClicked;
        NewSpacePopup.CancelClicked += OnPopupCancelClicked;
        NewSpacePopup.ChooseIconClicked += OnPopupChooseIconClicked;
        IconPicker.IconSelected += OnIconPickerIconSelected;

        UpdateGreeting();
    }

    private void UpdateGreeting()
    {
        int currentHour = DateTime.Now.Hour;
        
        if (currentHour >= 5 && currentHour < 12)
        {
            Title = "Good Morning, Eitan";
        }
        else if (currentHour >= 12 && currentHour < 6)
        {
            Title = "Good Afternoon, Eitan";
        }
        else if (currentHour >= 6 && currentHour < 10)
        {
            Title = "Good Evening, Eitan";
        }
        else
        {
            Title = "Good Night, Eitan";
        }
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
    
    private void OnPopupSaveClicked(object sender, EventArgs e)
    {
        // This is where the action happens. We get the data from the popup's properties.
        SpaceManager.Current.AddSpace(NewSpacePopup.SpaceName, NewSpacePopup.SpaceIcon);

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
    }


    #endregion
}