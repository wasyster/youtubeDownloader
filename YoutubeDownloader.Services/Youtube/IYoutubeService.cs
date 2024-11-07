namespace YoutubeDownloader.Services.Youtube;

public interface IYoutubeService
{
    Task DownloadAudioAsync(string videoURL, string fileName);
    Task DownloadVideoAsync(string videoURL, string fileName);
    Task<IReadOnlyCollection<IVideo>> GetPlaylistDataAsync(string videoURL);
    Task<IVideo> GetVideoDataAsync(string videoURL);
}