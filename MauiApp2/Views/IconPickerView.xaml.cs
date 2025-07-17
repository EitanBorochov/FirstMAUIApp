using System.Reflection;

namespace MauiApp2.Views;

public partial class IconPickerView : ContentView
{
    public event EventHandler<string> IconSelected;

    public IconPickerView()
    {
        InitializeComponent();
        LoadEmojis();
    }

    private void LoadEmojis()
    {
        var emojis = new List<string>();
        var resourcePath = "MauiApp2.Resources.Raw.emojis.txt";
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream is null) return;
        using var reader = new StreamReader(stream);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
            {
                // This correctly splits a line containing multiple emojis
                var stringInfo = new System.Globalization.StringInfo(line);
                for (int i = 0; i < stringInfo.LengthInTextElements; i++)
                {
                    emojis.Add(stringInfo.SubstringByTextElements(i, 1));
                }
            }
        }
        
        // Give the entire list of emojis to the CollectionView.
        // The CollectionView will handle displaying them efficiently.
        EmojiCollectionView.ItemsSource = emojis;
    }

    private void OnIconTapped(object sender, TappedEventArgs e)
    {
        string selectedIcon = e.Parameter as string;
        IconSelected?.Invoke(this, selectedIcon);
    }
}