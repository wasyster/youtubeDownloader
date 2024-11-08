namespace YoutubeDownloader.MauiApplication.ViewModels;

public partial class FileDownloadViewModel(IYoutubeService youtubeService) : SearchModel
{
    [ObservableProperty]
    private string currentState = StateContainerStates.Youtube.Empty;

    [ObservableProperty]
    private string videoStream;

    [ObservableProperty]
    private Video videoData;

    private Regex youtubeRegEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

    public IAsyncRelayCommand SearchCommand => new AsyncRelayCommand<string>(SearchAsync);

    public IAsyncRelayCommand DownloadCommand => new AsyncRelayCommand(DownloadAsync);

    private async Task SearchAsync(string videoUrl)
    {
        if (!IsModelValid())
        {
            CurrentState = StateContainerStates.Youtube.Empty;
            return;
        }

        var youtubeMatch = youtubeRegEx.Match(videoUrl);
        if (!youtubeMatch.Success)
        {
            CurrentState = StateContainerStates.Youtube.NotAYoutubeVideoLink;
            return;
        }

        try
        {
            VideoStream = await youtubeService.GetVideoStreamUrlAsync(videoUrl);
            VideoData = await youtubeService.GetVideoDataAsync(videoUrl) as Video;

            CurrentState = StateContainerStates.Youtube.Success;
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }
    }

    private async Task DownloadAsync()
    {

    }
}
