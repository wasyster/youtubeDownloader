namespace YoutubeDownloader.MauiApplication.Models;

public partial class SettingsModel : ObservableObject
{
	[ObservableProperty]
	private string saveFolder;
}
