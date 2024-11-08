namespace YoutubeDownloader.MauiApplication.AppConfigurations;

public static class DIConfiguration
{
	public static MauiAppBuilder AddDI(this MauiAppBuilder builder)
	{
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

		return builder;
	}
}
