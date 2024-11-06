namespace YoutubeDownloader.Services.Youtube;

public interface IYoutubeService
{
    Task DownloadAudioAsync(string videoURL);
    Task DownloadVideoAsync(string videoURL);
    Task<IReadOnlyCollection<IVideo>> GetPlaylistDataAsync(string videoURL);
    Task<IVideo> GetVideoDataAsync(string videoURL);
}