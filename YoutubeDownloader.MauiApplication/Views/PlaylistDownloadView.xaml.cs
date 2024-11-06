namespace YoutubeDownloader.MauiApplication.Views;

public partial class PlaylistDownloadView : ContentPage
{
    public static string Name => nameof(FileDownloadView);

    public PlaylistDownloadViewModel ViewModel => this.BindingContext as PlaylistDownloadViewModel;

    public PlaylistDownloadView(PlaylistDownloadViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}