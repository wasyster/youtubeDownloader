namespace YoutubeDownloader.MauiApplication.AppConfigurations;

public static class AppSettingsConfiguration
{
    public static MauiAppBuilder UseAppSettingFromJson(this MauiAppBuilder builder)
    {
        var file = "Resources.Raw.appSettings.json";

        var assembly = typeof(App).GetTypeInfo().Assembly;
        var assemblyName = assembly.GetName().Name.Replace(" ", "_");

        var stream = assembly.GetManifestResourceStream($"{assemblyName}.{file}");

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();


        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}
