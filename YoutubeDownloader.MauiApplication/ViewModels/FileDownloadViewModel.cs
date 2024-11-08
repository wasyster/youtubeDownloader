namespace YoutubeDownloader.MauiApplication.ViewModels;

public partial class FileDownloadViewModel(IYoutubeService youtubeService) : SearchModel
{
    [ObservableProperty]
    private string currentState = StateContainerStates.Youtube.Empty;

    [ObservableProperty]
    private string videoStream;

    [ObservableProperty]
    private SearchResult videoData;

    private Regex youtubeRegEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

    public IAsyncRelayCommand SearchCommand => new AsyncRelayCommand<string>(SearchAsync);

    public IAsyncRelayCommand DownloadAudioCommand => new AsyncRelayCommand(DownloadAudioAsync);

    public IAsyncRelayCommand DownloadVideoCommand => new AsyncRelayCommand(DownloadVideoAsync);

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
            var data = await youtubeService.GetVideoDataAsync(videoUrl);
            VideoURL.Value = videoUrl;
            VideoData = new SearchResult(data);

            CurrentState = StateContainerStates.Youtube.Success;
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }
    }

    private async Task DownloadAudioAsync()
    {
        CurrentState = StateContainerStates.Youtube.Downloading;

        try
        {
            await youtubeService.DownloadAudioAsync(VideoURL.Value, VideoData.Title);
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }

        CurrentState = StateContainerStates.Youtube.Success;
    }

    private async Task DownloadVideoAsync()
    {
        CurrentState = StateContainerStates.Youtube.Downloading;

        try
        {
            await youtubeService.DownloadVideoAsync(VideoURL.Value, VideoData.Title);
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }

        CurrentState = StateContainerStates.Youtube.Success;
    }
}
