namespace YoutubeDownloader.Services;

public interface IYoutubeService
{
	Task DownloadAudioAsync(string videoURL);
	Task DownloadVideoAsync(string videoURL);
	Task<IReadOnlyCollection<IVideo>> GetVideosDataAsync(string videoURL);
}