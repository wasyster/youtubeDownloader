namespace YoutubeDownloader.MauiApplication.ViewModels;

public partial class PlaylistDownloadViewModel(IYoutubeService youtubeService) : SearchModel
{
    [ObservableProperty]
    string currentState = StateContainerStates.Youtube.Empty;

    [ObservableProperty]
    private ObservableCollection<SearchResult> searchResults;

    [ObservableProperty]
    private bool selectAll;

    private Regex youtubeRegEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

    public IAsyncRelayCommand SearchCommand => new AsyncRelayCommand<string>(SearchCommandAsync);

    public IAsyncRelayCommand DownloadCommand => new AsyncRelayCommand(DownloadCommandAsync);

	public RelayCommand MarkAllCommand => new RelayCommand(MarkAll);

	private async Task SearchCommandAsync(string videoUrl)
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
            var playLists = await youtubeService.GetPlaylistDataAsync(videoUrl);
            SearchResults = playLists.Select(x => new SearchResult(x)).ToObservableCollection();

            CurrentState = StateContainerStates.Youtube.Success;
        }
        catch
        {
            CurrentState = StateContainerStates.Youtube.Error;
        }
    }

    private async Task DownloadCommandAsync()
    {
        CurrentState = StateContainerStates.Youtube.Loading;

        try
        {
            var tasks = new Task[SearchResults.Count];
            using var semaphoreSlim = new SemaphoreSlim(4);

            for (int i = 0; i < SearchResults.Count; ++i)
            {
                var searchResult = SearchResults[i - 1];

                if (!searchResult.Download)
                {
                    continue;
                }

                async Task DownloadAsync()
                {
                    try
                    {
                        if (searchResult.OnlyAudio)
                        {
                            await youtubeService.DownloadAudioAsync(searchResult.Url);
                        }
                        else
                        {
                            await youtubeService.DownloadVideoAsync(searchResult.Url);
                        }
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                }

                await semaphoreSlim.WaitAsync();
                tasks[i] = DownloadAsync();
            }

            await Task.WhenAll(tasks);

            CurrentState = StateContainerStates.Youtube.Success;
        }
        catch
        {
			CurrentState = StateContainerStates.Youtube.Error;
		}
    }

    private void MarkAll()
    {
        foreach(var searcResult in this.SearchResults)
        {
            searcResult.Download = this.SelectAll;
        }
    }
}
