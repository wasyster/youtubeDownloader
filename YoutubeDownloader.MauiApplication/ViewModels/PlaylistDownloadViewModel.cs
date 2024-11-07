﻿namespace YoutubeDownloader.MauiApplication.ViewModels;

public partial class PlaylistDownloadViewModel(IYoutubeService youtubeService) : SearchModel
{
    [ObservableProperty]
    string currentState = StateContainerStates.Youtube.Empty;

    [ObservableProperty]
    private ObservableCollection<SearchResult> searchResults;

    [ObservableProperty]
    private bool selectAll;

    [ObservableProperty]
    private bool canDownload = false;

	[ObservableProperty]
	private bool canSelectAll = false;

	private Regex youtubeRegEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

    public IAsyncRelayCommand SearchCommand => new AsyncRelayCommand<string>(SearchCommandAsync);

    public IAsyncRelayCommand DownloadCommand => new AsyncRelayCommand(DownloadAsync);

	public IRelayCommand MarkAllCommand => new RelayCommand(MarkAll);

	public IRelayCommand OnSelectCommand => new RelayCommand(OnSelect);

	private async Task SearchCommandAsync(string videoUrl)
    {
        CurrentState = StateContainerStates.Youtube.Loading;
		CanSelectAll = false;

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

        CanSelectAll = true;
    }

    private async Task DownloadAsync()
    {
		var hasSelectedElements = SearchResults?.Any(x => x.Download) ?? false;

		if (!hasSelectedElements)
        {
            return;
        }

        try
        {   
			var parallelOptions = new ParallelOptions()
			{
				MaxDegreeOfParallelism = 4,
				CancellationToken = new CancellationToken(),
			};

			CurrentState = StateContainerStates.Youtube.Downloading;

            //await Parallel.ForEachAsync(SearchResults.Where(x => x.Download), parallelOptions, async (searchResult, ct) =>
            //         {
            //             if (searchResult.OnlyAudio)
            //             {
            //                 await youtubeService.DownloadAudioAsync(searchResult.Url, searchResult.Title);
            //             }
            //             else
            //             {
            //                 await youtubeService.DownloadVideoAsync(searchResult.Url, searchResult.Title);
            //             }
            //         });

            await Task.Delay(10000000);


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

        CanDownload = this.SelectAll;
	}

    private void OnSelect() => this.CanDownload = this.SearchResults.Any(x => x.Download);
}
