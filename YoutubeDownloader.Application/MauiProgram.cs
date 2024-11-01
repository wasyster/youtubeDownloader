namespace MauiApplication;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .UseMauiCommunityToolkitMarkup()
               .UseCupertinoMauiIcons()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<MainPage>();

        builder.Services.AddSingleton<YoutubeClient>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
