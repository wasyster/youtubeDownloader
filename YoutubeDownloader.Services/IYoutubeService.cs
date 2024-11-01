namespace ServicesProject;

public interface IYoutubeService
{
    Task<IReadOnlyList<PlaylistVideo>> DownloadAsync(string videoURL);
}