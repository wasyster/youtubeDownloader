﻿namespace YoutubeDownloader.Services.Youtube;

public class YoutubeService(YoutubeClient youtubeClient) : IYoutubeService
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
