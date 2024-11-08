namespace YoutubeDownloader.MauiApplication.AppConfigurations;

public static class FontConfiguration
{
	public static MauiAppBuilder AddFontConfiguration(this MauiAppBuilder builder)
	{
		builder.ConfigureFonts(fonts =>
		{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		});

		return builder;
	}
}
