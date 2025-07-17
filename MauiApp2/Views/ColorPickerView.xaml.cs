using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Views;

public partial class ColorPickerView : ContentView
{
    public event EventHandler<Color> ColorSelected;

    public Color[] ColorPalette { get; } = new[]
    {
        Color.FromArgb("#f94144"),
        Color.FromArgb("#f3722c"),
        Color.FromArgb("#f8961e"),
        Color.FromArgb("#f9c74f"),
        Color.FromArgb("#90be6d"),
        Color.FromArgb("#43aa8b"),
        Color.FromArgb("#577590")
    };

    public ColorPickerView()
    {
        InitializeComponent();
        this.BindingContext = this;
    }

    private void OnColorTapped(object? sender, EventArgs e)
    {
        if (sender is Button tappedButton)
        {
            Color selectedColor = tappedButton.BackgroundColor;
            
            ColorSelected?.Invoke(this, selectedColor);
        }
    }
}