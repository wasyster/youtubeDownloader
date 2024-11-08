namespace YoutubeDownloader.MauiApplication.ViewModels;

[ObservableObject]
public partial class SettingsViewModel(IDbContextService<SettingsModel> dbSettingsContext)
{
    [ObservableProperty]
    private SettingsModel settings;

    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);

    public IAsyncRelayCommand ChangeDownloadFolderCommand => new AsyncRelayCommand(ChangeDownloadFolderAsync);

    private async Task OnAppearingkAsync()
    {
        Settings = await dbSettingsContext.GetAsync(DatabeseKeys.Settings);
    }

    private async Task ChangeDownloadFolderAsync()
    {
        var result = await FolderPicker.Default.PickAsync();
        if (!result.IsSuccessful)
        {
            await Toast.Make($"The folder was not picked").Show();
            await Task.Delay(1000);
        }
        else
        {
            await dbSettingsContext.CreateOrUpdateIfExistsAsync(new SettingsModel(result));
        }
    }
}
