namespace YoutubeDownloader.MauiApplication.AppConfigurations;

public static class PlatformHanlderConfiguration
{
	public static void ConfigureOnlyAudioSwitchHandler()
	{
#if WINDOWS
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
	}
}
