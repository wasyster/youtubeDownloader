namespace YoutubeDownloader.MauiApplication;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .UseMauiCommunityToolkitMarkup()
               .UseCupertinoMauiIcons()
               .USeAppSettingFromJson()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

		var dbSettings = builder.Configuration.GetRequiredSection("DBSettings").Get<DBSettings>();
		builder.Services.AddSingleton<DBSettings>(dbSettings);

		builder.Services.AddTransient<FileDownloadViewModel>();
		builder.Services.AddTransient<PlaylistDownloadViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();

		builder.Services.AddTransient<FileDownloadView>();
		builder.Services.AddTransient<PlaylistDownloadView>();
		builder.Services.AddTransient<SettingsView>();

		builder.Services.AddTransient<YoutubeClient>();
        builder.Services.AddTransient<IYoutubeService, YoutubeService>();
        builder.Services.AddTransient<IDbContextService<SettingsModel>, DbContextService<SettingsModel>>();

#if WINDOWS
        //full screen
        //builder.ConfigureLifecycleEvents(events =>
        //{
        //    // Make sure to add "using Microsoft.Maui.LifecycleEvents;" in the top of the file 
        //    events.AddWindows(windowsLifecycleBuilder =>
        //    {
        //        windowsLifecycleBuilder.OnWindowCreated(window =>
        //        {
        //            window.ExtendsContentIntoTitleBar = false;
        //            var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        //            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        //            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
        //            switch (appWindow.Presenter)
        //            {
        //                case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
        //                    overlappedPresenter.SetBorderAndTitleBar(true, true);
        //                    overlappedPresenter.Maximize();
        //                    break;
        //            }
        //        });
        //    });
        //});

        Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("OnlyAudio", (handler, view) =>
        {
            // Remove this if statement if you want to apply this to all switches
            if (view is OnlyAudioSwitch)
            {
                handler.PlatformView.OnContent = "Audio only";
                handler.PlatformView.OffContent = "Video";

                // Add this to remove the padding around the switch as well
                // handler.PlatformView.MinWidth = 0;
            }
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
