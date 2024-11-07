namespace YoutubeDownloader.MauiApplication.Constants;

public static partial class StateContainerStates
{
    public static class Youtube
    {
        public const string Loading = nameof(Loading);
		public const string Downloading = nameof(Downloading);
		public const string Success = nameof(Success);
        public const string Empty = nameof(Empty);
        public const string Error = nameof(Error);
        public const string NotAYoutubeVideoLink = nameof(NotAYoutubeVideoLink);
    }
}
