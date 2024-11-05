using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader.Services;

public class YoutubeService(YoutubeClient youtubeClient) : IYoutubeService
{
    public async Task<IReadOnlyList<PlaylistVideo>> GetVideosDataAsync(string videoURL)
    {
        var videos = await youtubeClient.Playlists.GetVideosAsync(videoURL);

        return videos;
    }

    public async Task DownloadVideoAsync(string videoURL)
    {
		var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoURL);

		var streamInfo = streamManifest.GetVideoOnlyStreams()
									.Where(s => s.Container == Container.Mp4)
									.GetWithHighestVideoQuality();

		await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
	}

	public async Task DownloadAudioAsync(string videoURL)
	{
		var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoURL);
		var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
		await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
	}
}
