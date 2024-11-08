namespace YoutubeDownloader.MauiApplication;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .UseMauiCommunityToolkitMarkup()
               .UseMauiCommunityToolkitMediaElement()
               .UseCupertinoMauiIcons()
               .UseAppSettingFromJson()
               .AddDI()
               .AddFontConfiguration();

        PlatformHanlderConfiguration.ConfigureOnlyAudioSwitchHandler();

#if DEBUG
		builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
