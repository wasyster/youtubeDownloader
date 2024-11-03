namespace YoutubeDownloader.Services;

public interface IYoutubeService
{
    Task<IReadOnlyList<PlaylistVideo>> DownloadAsync(string videoURL);
}