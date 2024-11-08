namespace YoutubeDownloader.Services.Youtube;

public class YoutubeService(YoutubeClient youtubeClient, IDbContextService<SettingsModel> dbSettingsContext) : IYoutubeService
{
    public async Task<IVideo> GetVideoDataAsync(string videoURL)
    {
        var video = await youtubeClient.Videos.GetAsync(videoURL);
        return video;
    }

    public async Task<IReadOnlyCollection<IVideo>> GetPlaylistDataAsync(string videoURL)
    {
        var playlist = await youtubeClient.Playlists.GetVideosAsync(videoURL);
        return playlist;
    }

    public async Task DownloadVideoAsync(string videoURL, string fileName)
    {
		var normalizedFileName = await GetFilePathAsync(fileName, "mp4");

		var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoURL);

        var streamInfo = streamManifest.GetVideoOnlyStreams()
                                    .Where(s => s.Container == Container.Mp4)
                                    .GetWithHighestVideoQuality();

        await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, normalizedFileName);
    }

    public async Task DownloadAudioAsync(string videoURL, string fileName)
    {
		var normalizedFileName = await GetFilePathAsync(fileName, "mp3");

		var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoURL);
        var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, normalizedFileName);
    }

    private async Task<string> GetFilePathAsync(string fileName, string extension)
    {
        var settings = await dbSettingsContext.GetAsync(DatabeseKeys.Settings);

		var root = settings?.SaveFolder?.Folder?.Path!;
        var normalizedFileName = fileName.Replace('"', ' ').Replace('|', ' ').Replace('?', ' ').Replace(@"/", "").Replace(@":", "");
		var fullPath = $"{root}/{normalizedFileName}.{extension}";

		return fullPath;
	}
}
