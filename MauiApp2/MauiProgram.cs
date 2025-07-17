using Microsoft.Extensions.Logging;

namespace MauiApp2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // Text fonts
                fonts.AddFont("Text/Text-Black.ttf", "TextBlack");
                fonts.AddFont("Text/Text-Bold.ttf", "TextBold");
                fonts.AddFont("Text/Text-ExtraBold.ttf", "TextExtraBold");
                fonts.AddFont("Text/Text-ExtraLight.ttf", "TextExtraLight");
                fonts.AddFont("Text/Text-Italic.ttf", "TextItalic");
                fonts.AddFont("Text/Text-Light.ttf", "TextLight");
                fonts.AddFont("Text/Text-LightItalic.ttf", "TextLightItalic");
                fonts.AddFont("Text/Text-Regular.ttf", "TextRegular");

                // Title fonts
                fonts.AddFont("Title/Title-Bold.ttf", "TitleBold");
                fonts.AddFont("Title/Title-ExtraBold.ttf", "TitleExtraBold");
                fonts.AddFont("Title/Title-Light.ttf", "TitleLight");
                fonts.AddFont("Title/Title-Medium.ttf", "TitleMedium");
                fonts.AddFont("Title/Title-Regular.ttf", "TitleRegular");
                fonts.AddFont("Title/Title-SemiBold.ttf", "TitleSemiBold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}