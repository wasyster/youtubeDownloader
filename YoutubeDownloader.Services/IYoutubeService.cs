namespace YoutubeDownloader.Services;

public interface IYoutubeService
{
    Task<IReadOnlyList<PlaylistVideo>> GetVideosDataAsync(string videoURL);
}