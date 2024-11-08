namespace YoutubeDownloader.Shared.Models;

public partial class SearchResult : ObservableObject, IVideo
{

    #region IVideo
    //
    // Summary:
    //     Video ID.
    public VideoId Id { get; }

    //
    // Summary:
    //     Video URL.
    public string Url { get; }

    //
    // Summary:
    //     Video title.
    public string Title { get; }

    //
    // Summary:
    //     Video author.
    public Author Author { get; }

    //
    // Summary:
    //     Video duration.
    //
    // Remarks:
    //     May be null if the video is a currently ongoing live stream.
    public TimeSpan? Duration { get; }

    //
    // Summary:
    //     Video thumbnails.
    public IReadOnlyList<Thumbnail> Thumbnails { get; }
    #endregion

    [ObservableProperty]
    private bool download;

    public string Thumbnail => this.Thumbnails.GetWithHighestResolution().Url;


	public SearchResult()
    {
    }

    public SearchResult(VideoId id, string url, string title, Author author, TimeSpan? duration, IReadOnlyList<Thumbnail> thumbnails)
    {
        Id = id;
        Url = url;
        Title = title;
        Author = author;
        Duration = duration;
        Thumbnails = thumbnails;
    }

    public SearchResult(IVideo video)
    {
        if (video is null)
        {
            return;
        }

        Id = video.Id;
        Url = video.Url;
        Title = video.Title;
        Author = video.Author;
        Duration = video.Duration;
        Thumbnails = video.Thumbnails;
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override string ToString() => $"Playlist ({Title})";
}
