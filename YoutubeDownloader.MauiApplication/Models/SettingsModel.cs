namespace YoutubeDownloader.MauiApplication.Models;

public partial class SettingsModel : ObservableObject, IEntity
{
    public string Id { get;  set; }

    [ObservableProperty]
	private string saveFolder;
}
