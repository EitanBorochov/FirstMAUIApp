namespace MauiApp2;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
    
        if (count == 1)
            ClickButton.Text = $"Clicked {count} time";
        else
            ClickButton.Text = $"Clicked {count} times";
    
        SemanticScreenReader.Announce(ClickButton.Text);
    }
}