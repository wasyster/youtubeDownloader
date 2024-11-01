namespace ServicesProject;

public class YoutubeService(YoutubeClient youtubeClient) : IYoutubeService
{
    public async Task<IReadOnlyList<PlaylistVideo>> DownloadAsync(string videoURL)
    {
        var videos = await youtubeClient.Playlists.GetVideosAsync(videoURL);

        return videos;    }
}
