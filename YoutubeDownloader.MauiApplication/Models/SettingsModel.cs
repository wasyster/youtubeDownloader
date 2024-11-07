namespace YoutubeDownloader.MauiApplication.Models;

public partial class SettingsModel : ObservableObject, IEntity
{
    public string Id { get;  set; }

    [ObservableProperty]
	private FolderPickerResult saveFolder;

    public SettingsModel()
    {
        this.Id = DatabeseKeys.Settings;
    }

    public SettingsModel(FolderPickerResult saveFolder): this()
    {
        this.SaveFolder = saveFolder;
    }
}
