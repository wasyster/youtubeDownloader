namespace YoutubeDownloader.MauiApplication.ViewModels;

public partial class MainPageViewModel(IYoutubeService youtubeService) : SearchModel
{
    [ObservableProperty]
    string currentState = StateContainerStates.Youtube.Empty;

    [ObservableProperty]
    private ObservableCollection<SearchResult> searchResults;

    private Regex youtubeRegEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
    private string isYoututbePlaylistPattern = @"(youtube\.com\/playlist\?list=|youtu\.be\/.*\?list=)";

    public IAsyncRelayCommand SearchCommand => new AsyncRelayCommand<string>(SearchCommandAsnc);

    private async Task SearchCommandAsnc(string videoUrl)
    {
        CurrentState = StateContainerStates.Youtube.Loading;

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
            var playLists = await youtubeService.GetVideosDataAsync(videoUrl);
            SearchResults = playLists.Select(x => new SearchResult(x)).ToObservableCollection();

            CurrentState = StateContainerStates.Youtube.Success;
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }
    }

    private bool IsPlaylistUrl(string url) => Regex.IsMatch(url, isYoututbePlaylistPattern);
}
