namespace YoutubeDownloader.Services;

public class YoutubeService(YoutubeClient youtubeClient) : IYoutubeService
{
	private static string isYoututbePlaylistPattern = @"(youtube\.com\/playlist\?list=|youtu\.be\/.*\?list=)";

	public async Task<IReadOnlyCollection<IVideo>> GetVideosDataAsync(string videoURL)
    {
		var isUrlPlaylist = IsPlaylistUrl(videoURL);
		var result = new List<IVideo>();

		try
		{
			if (isUrlPlaylist)
			{
				var videos = await youtubeClient.Playlists.GetVideosAsync(videoURL);

				foreach(var video in videos)
				{
					result.Add(video);
				}
			}
			else
			{
				var video = await youtubeClient.Videos.GetAsync(videoURL);
				result.Add(video);
			}
		}
		catch
		{
			var videos = await youtubeClient.Playlists.GetVideosAsync(videoURL);

			foreach (var video in videos)
			{
				result.Add(video);
			}
		}

		return result;
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

	private bool IsPlaylistUrl(string url) => Regex.IsMatch(url, isYoututbePlaylistPattern);
}
